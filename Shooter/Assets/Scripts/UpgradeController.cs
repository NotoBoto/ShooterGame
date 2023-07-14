using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeController : MonoBehaviour
{
    private PlayerController _player;
    private LvlController _lvlController;
    private EventTrigger _eventTrigger;

    public int UpgradeType;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        _lvlController = FindObjectOfType<LvlController>();
        _eventTrigger = GetComponent<EventTrigger>();
        _eventTrigger.enabled = false;
        Invoke("EventTrigger", 0.75f);
    }

    private void EventTrigger()
    {
        _eventTrigger.enabled = true;
    }

    public void Upgrade()
    {
        if (UpgradeType == 0 && _player.PlayerModel.ShotCooldown > 0.5f)
        {
            _player.PlayerModel.ShotCooldown -= 0.25f;
        }
        else if (UpgradeType == 1)
        {
            _player.PlayerModel.ArrowDamage += 0.5f;
        }
        else if (UpgradeType == 2)
        {
            _player.PlayerModel.ArrowRange += 0.25f;
        }
        else if (UpgradeType == 3)
        {
            _player.PlayerModel.ArrowSpeed++;
        }
        else if (UpgradeType == 4 && _player.PlayerModel.ShotCooldown > 0.1f)
        {
            _player.PlayerModel.ShotCooldown -= 0.5f;
            if(_player.PlayerModel.ShotCooldown < 0.1f)
            {
                _player.PlayerModel.ShotCooldown = 0.1f;
            }
        }
        else if (UpgradeType == 5)
        {
            _player.PlayerModel.ArrowDamage += 1f;
        }
        else if (UpgradeType == 6)
        {
            _player.PlayerModel.ArrowPiercing++;
        }
        else if (UpgradeType == 7)
        {
            _player.PlayerModel.ArrowRange += 0.5f;
        }
        else if (UpgradeType == 8)
        {
            _player.PlayerModel.ArrowSpeed += 2f;
        }
        _lvlController.GotSkill();
    }
}
