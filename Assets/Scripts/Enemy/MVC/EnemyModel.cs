using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : MonoBehaviour, IEnemyModel
{
    public float speed;

    Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 dir)
    {
        dir *= speed;
        _rb.velocity = dir;
    }
}
