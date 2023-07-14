using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlController : MonoBehaviour
{
    private AudioSource _lvlAudioSource;

    private PlayerController _player;
    private Transform[] _spawners;
    private GameObject[] _slimePrefabs;
    private GameObject[] _upgradePrefabs;
    private AudioClip[] _lvlMusicClips;
    public Transform Upgrade1, Upgrade2, Upgrade3;
    [HideInInspector]
    public GameObject UpgradeGameobject1, UpgradeGameobject3, UpgradeGameobject2;

    private float _spawnCooldown;
    private float _lastSpawnTime;
    private bool _isWaitingForInputToStart;
    [HideInInspector]
    public bool IsWaitingForInputToRestart;

    private void Awake()
    {
        _lvlAudioSource = GetComponent<AudioSource>();

        _player = FindObjectOfType<PlayerController>();
        _spawners = GetComponentsInChildren<Transform>();
        _slimePrefabs = Resources.LoadAll<GameObject>("SlimePrefabs");
        _upgradePrefabs = Resources.LoadAll<GameObject>("UpgradePrefabs");
        _lvlMusicClips = Resources.LoadAll<AudioClip>("Sound/Music");
        _player.PlayerModel.IsGameOn = false;

        _spawnCooldown = 2f;
        _lastSpawnTime = -_spawnCooldown;
        _isWaitingForInputToStart = true;
        IsWaitingForInputToRestart = false;

        PlayMusicClip();
    }

    private void Update()
    {
        if (_isWaitingForInputToStart)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

            if (Input.anyKeyDown)
            {
                _isWaitingForInputToStart = false;
                _player.PlayerModel.IsGameOn = true;
                _player.TargetTransform.gameObject.SetActive(true);
                _player.Canvas.transform.Find("StartingText").gameObject.SetActive(false);
                _player.Canvas.gameObject.SetActive(false);
            }
        }

        if (_player.PlayerModel.IsGameOn)
        {
            if (Time.time - _lastSpawnTime >= _spawnCooldown)
            {
                _lastSpawnTime = Time.time;
                SpawnSlime();
                if (_spawnCooldown > 0.1f)
                {
                    _spawnCooldown -= 0.005f;
                }
            }
        }

        if (!_isWaitingForInputToStart && IsWaitingForInputToRestart && Input.anyKey)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void SpawnSlime()
    {
        GameObject _slime; 
        Transform _spawner = _spawners[Random.Range(1, _spawners.Length)];
        if (_spawnCooldown > 1.9)
        {
            _slime = _slimePrefabs[0];
        }
        else if(_spawnCooldown > 1.7)
        {
            if(Random.Range(0, 2) == 0)
            {
                _slime = _slimePrefabs[1];
            }
            else
            {
                _slime = _slimePrefabs[0];
            }
        }
        else if (_spawnCooldown > 1.6)
        {
            _slime = _slimePrefabs[Random.Range(0, 1)];
        }
        else if (_spawnCooldown > 1.5)
        {
            if (Random.Range(0, 2) == 0)
            {
                _slime = _slimePrefabs[0];
            }
            else
            {
                _slime = _slimePrefabs[1];
            }
        }
        else if (_spawnCooldown > 1.4)
        {
            _slime = _slimePrefabs[1];
        }
        else if (_spawnCooldown > 1.3)
        {
            if (Random.Range(0, 2) == 0)
            {
                _slime = _slimePrefabs[0];
            }
            else if (Random.Range(0, 2) > 0)
            {
                _slime = _slimePrefabs[1];
            }
            else
            {
                _slime = _slimePrefabs[2];
            }
        }
        else if (_spawnCooldown > 1.1)
        {
            if (Random.Range(0, 2) == 0)
            {
                _slime = _slimePrefabs[1];
            }
            else if (Random.Range(0, 2) > 0)
            {
                _slime = _slimePrefabs[2];
            }
            else
            {
                _slime = _slimePrefabs[0];
            }
        }
        else
        {
            _slime = _slimePrefabs[Random.Range(0, _slimePrefabs.Length)];
        }
        GameObject Slime = Instantiate(_slime, _spawner.position, _spawner.rotation);
    }

    public void NewSkill()
    {
        Cursor.visible = true;
        _player.Canvas.gameObject.SetActive(true);
        _player.Canvas.transform.Find("Upgrade").gameObject.SetActive(true);

        UpgradeGameobject1 = Instantiate(_upgradePrefabs[Random.Range(0, _upgradePrefabs.Length)], Upgrade1.position, Upgrade1.rotation);
        UpgradeGameobject1.transform.SetParent(Upgrade1.transform, true);

        UpgradeGameobject2 = Instantiate(_upgradePrefabs[Random.Range(0, _upgradePrefabs.Length)], Upgrade2.position, Upgrade2.rotation);
        UpgradeGameobject2.transform.SetParent(Upgrade2.transform, true);

        UpgradeGameobject3 = Instantiate(_upgradePrefabs[Random.Range(0, _upgradePrefabs.Length)], Upgrade3.position, Upgrade2.rotation);
        UpgradeGameobject3.transform.SetParent(Upgrade3.transform, true);
    }

    public void GotSkill()
    {
        Cursor.visible = false;
        _player.Canvas.transform.GetChild(2).gameObject.SetActive(false);
        _player.Canvas.gameObject.SetActive(false);
        Destroy(UpgradeGameobject1 as GameObject);
        Destroy(UpgradeGameobject2 as GameObject);
        Destroy(UpgradeGameobject3 as GameObject);
    }

    private void PlayMusicClip()
    {
        if (_lvlMusicClips.Length > 0)
        {
            int r = Random.Range(0, _lvlMusicClips.Length);
            _lvlAudioSource.clip = _lvlMusicClips[r];
            _lvlAudioSource.Play();

            Invoke("PlayMusicClip", _lvlMusicClips[r].length);
        }
    }
}