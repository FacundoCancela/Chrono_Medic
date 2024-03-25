using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    IEnemyModel _enemy;
    IEnemyView _view;
    Transform _playerTransform; // Referencia al transform del jugador

    public int maxHealth = 10;
    public int actualHealth;

    private void Awake()
    {
        _enemy = GetComponent<EnemyModel>();
        _view = GetComponent<EnemyView>();
    }

    private void Start()
    {
        actualHealth = maxHealth;
        _playerTransform = FindObjectOfType<PlayerController>().transform; 
    }

    private void Update()
    {
        if (actualHealth <= 0)
        {
            Die();
        }

        FollowPlayer(); // Llama a la función FollowPlayer en cada frame
    }

    public void GetDamaged(int damage)
    {
        _view.GetDamaged();
        actualHealth -= damage;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    void FollowPlayer()
    {
        if (_playerTransform != null)
        {
            Vector2 dir = _playerTransform.position - transform.position;
            dir.Normalize(); // Normaliza para que el enemigo no se mueva más rápido al acercarse al jugador
            _enemy.Move(dir); // Mueve al enemigo hacia la dirección del jugador
        }
    }
}
