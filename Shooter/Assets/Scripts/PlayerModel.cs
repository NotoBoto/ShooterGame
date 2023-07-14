using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    private float _speed;
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    private float _shotCooldown;
    public float ShotCooldown
    {
        get { return _shotCooldown; }
        set { _shotCooldown = value; }
    }

    private int _lvl;
    public int Lvl
    {
        get { return _lvl; }
        set { _lvl = value; }
    }

    private float _experience;
    public float Experience
    {
        get { return _experience; }
        set { _experience = value; }
    }

    private bool _isGameOn;
    public bool IsGameOn
    {
        get { return _isGameOn; }
        set { _isGameOn = value; }
    }

    private float _arrowSpeed;
    public float ArrowSpeed
    {
        get { return _arrowSpeed; }
        set { _arrowSpeed = value; }
    }

    private float _arrowDamage;
    public float ArrowDamage
    {
        get { return _arrowDamage; }
        set { _arrowDamage = value; }
    }

    private float _arrowPiercing;
    public float ArrowPiercing
    {
        get { return _arrowPiercing; }
        set { _arrowPiercing = value; }
    }

    private float _arrowRange;
    public float ArrowRange
    {
        get { return _arrowRange; }
        set { _arrowRange = value; }
    }

    private float _score;
    public float Score
    {
        get { return _score; }
        set { _score = value; }
    }
}
