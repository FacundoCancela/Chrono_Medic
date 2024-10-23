using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, IActorModel
{
    Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Move(Vector2 dir)
    {
        _rb.velocity = dir;
    }
}
