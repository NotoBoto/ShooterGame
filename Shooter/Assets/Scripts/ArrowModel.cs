using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowModel
{
    private float _speed;
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    private float _damage;
    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    private float _piercing;
    public float Piercing
    {
        get { return _piercing; }
        set { _piercing = value; }
    }

    private float _range;
    public float Range
    {
        get { return _range; }
        set { _range = value; }
    }
}
