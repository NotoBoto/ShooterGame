using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel
{
    private float _hp;
    public float HP
    {
        get { return _hp; }
        set { _hp = value; }
    }

    private float _dashForce;
    public float DashForce
    {
        get { return _dashForce; }
        set { _dashForce = value; }
    }

    private float _dashDuration;
    public float DashDuration
    {
        get { return _dashDuration; }
        set { _dashDuration = value; }
    }

    private float _dashCooldown;
    public float DashCooldown
    {
        get { return _dashCooldown; }
        set { _dashCooldown = value; }
    }

    private bool _isDashing;
    public bool IsDashing
    {
        get { return _isDashing; }
        set { _isDashing = value; }
    }

    private float _experience;
    public float Experience
    {
        get { return _experience;}
        set { _experience = value;}
    }
}
