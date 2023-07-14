using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    public EnemyModel EnemyModel;
    private Rigidbody2D _enemyRigidbody;

    private PlayerController _player;

    public float HP;
    public float DashForece;
    public float DashDuration;
    public float DashCooldown;
    public float Experience;

    private void Awake()
    {
        EnemyModel = new EnemyModel();
        _enemyRigidbody = GetComponent<Rigidbody2D>();

        _player = FindAnyObjectByType<PlayerController>();

        EnemyModel.HP = HP;
        EnemyModel.DashForce = DashForece;
        EnemyModel.DashDuration = DashDuration;
        EnemyModel.DashCooldown = DashCooldown;
        EnemyModel.IsDashing = false;
        EnemyModel.Experience = Experience;

        StartCoroutine(PerformPeriodicDash());
    }

    private IEnumerator PerformPeriodicDash()
    {
        while (true)
        {
            yield return new WaitForSeconds(EnemyModel.DashCooldown);

            if (!EnemyModel.IsDashing)
            {
                LookAtTarget(_player.transform);
                Vector2 dashDirection = transform.right;

                StartCoroutine(PerformDash(dashDirection));
            }
        }
    }

    private IEnumerator PerformDash(Vector2 dashDirection)
    {
        EnemyModel.IsDashing = true;

        _enemyRigidbody.AddForce(dashDirection * EnemyModel.DashForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(EnemyModel.DashDuration);
        _enemyRigidbody.velocity = Vector2.zero;
        EnemyModel.IsDashing = false;
    }

    private void LookAtTarget(Transform target)
    {
        Vector3 direction = target.position - transform.position;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1000f * Time.deltaTime);
    }
}
