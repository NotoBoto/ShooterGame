using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerModel PlayerModel;
    private Rigidbody2D _playerRigidbody;
    private Transform _bowTransform;
    [HideInInspector]
    public Transform TargetTransform;
    [HideInInspector]
    public Canvas Canvas;
    private LvlController _lvlController;
    private AudioSource _playerAudioSource;

    public GameObject ArrowPrefab;

    private AudioClip[] _SFXAudioClips;

    private float _lastShotTime;

    private float _timer;

    private void Awake()
    {
        PlayerModel = new PlayerModel();
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _bowTransform = transform.Find("Bow");
        TargetTransform = transform.Find("Target");
        Canvas = FindObjectOfType<Canvas>();
        _lvlController = FindObjectOfType<LvlController>();
        _playerAudioSource = GetComponent<AudioSource>();

        PlayerModel.ShotCooldown = 3f;
        PlayerModel.Lvl = 0;
        PlayerModel.Experience = 4f;
        PlayerModel.ArrowSpeed = 4f;
        PlayerModel.ArrowDamage = 1f;
        PlayerModel.ArrowPiercing = 1f;
        PlayerModel.ArrowRange = 1f;
        PlayerModel.Score = 0f;
        PlayerModel.Speed = 4f;

        _SFXAudioClips = Resources.LoadAll<AudioClip>("Sound/SFX");

        _lastShotTime = -PlayerModel.ShotCooldown;
    }

    private void Update()
    {
        if(PlayerModel.IsGameOn)
        {
            Movement(PlayerModel.Speed);
            BowAtTarget(TargetTransform);

            if (Time.time - _lastShotTime >= PlayerModel.ShotCooldown)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Shot();
                    _lastShotTime = Time.time;
                }
            }
        }

    }

    private void Movement(float speed)
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 movement = new(moveX, moveY);
        movement.Normalize();

        _playerRigidbody.velocity = movement * speed;

        _timer += Time.deltaTime;
        if(moveX != 0f || moveY != 0f)
        {
            if (_timer >= 0.5f)
            {
                _playerAudioSource.PlayOneShot(_SFXAudioClips[Random.Range(0, 8)]);
                _timer = 0f;
            }
        }
    }

    private void BowAtTarget(Transform target)
    {
        Vector3 direction = target.position - _bowTransform.position;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

        _bowTransform.rotation = Quaternion.Slerp(_bowTransform.rotation, targetRotation, 1000f * Time.deltaTime);
    }

    private void Shot()
    {
        _playerAudioSource.PlayOneShot(_SFXAudioClips[Random.Range(9, 13)]);
        Vector3 spawnPosition = _bowTransform.position + _bowTransform.right * 1f;
        GameObject Arrow = Instantiate(ArrowPrefab, spawnPosition, _bowTransform.rotation);
        ArrowController _arrow = Arrow.GetComponent<ArrowController>();
        _arrow.ArrowModel.Speed = PlayerModel.ArrowSpeed;
        _arrow.ArrowModel.Damage = PlayerModel.ArrowDamage;
        _arrow.ArrowModel.Piercing = PlayerModel.ArrowPiercing;
        _arrow.ArrowModel.Range = PlayerModel.ArrowRange;
    }

    public void CheckNewLvl()
    {
        if(PlayerModel.Experience >= Mathf.Sqrt(PlayerModel.Lvl)*5 + 5)
        {
            PlayerModel.Lvl++;
            PlayerModel.Experience = 0f;
            _lvlController.NewSkill();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        Cursor.visible = true;
        Canvas.gameObject.SetActive(true);
        Canvas.transform.Find("RestartingText").gameObject.SetActive(true);
        Canvas.transform.Find("RestartingText").Find("lvl").gameObject.GetComponent<TextMeshProUGUI>().text = "Your lvl: " + PlayerModel.Lvl;
        Canvas.transform.Find("RestartingText").Find("score").gameObject.GetComponent<TextMeshProUGUI>().text = "Your score: " + PlayerModel.Score;
        PlayerModel.IsGameOn = false;
        _playerRigidbody.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(0.5f);
        _lvlController.IsWaitingForInputToRestart = true;
    }
}
