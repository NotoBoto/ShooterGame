using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public ArrowModel ArrowModel;

    private PlayerController _player;

    private float _timer;
    private void Awake()
    {
        ArrowModel = new ArrowModel();

        _player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer <= ArrowModel.Range)
        {
            transform.Translate(Vector2.right * ArrowModel.Speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController _enemy = collision.gameObject.GetComponent<EnemyController>();
            _enemy.EnemyModel.HP -= ArrowModel.Damage;
            if(_enemy.EnemyModel.HP <= 0)
            {
                _player.PlayerModel.Score += _enemy.EnemyModel.Experience * 10;
                _player.PlayerModel.Experience += _enemy.EnemyModel.Experience;
                _player.CheckNewLvl();
                Destroy(_enemy.gameObject);
            }
            ArrowModel.Piercing--;
            if(ArrowModel.Piercing <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
