using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    IEnemyModel _enemy;
    IEnemyView _view;
    PlayerController _player;
    Transform _playerTransform; // Referencia al transform del jugador
    bool canAttack = true; // Variable para controlar el cooldown del ataque
    public float attackRange = 1f; // Rango de ataque del enemigo

    public int maxHealth = 10;
    public int actualHealth;
    public int damage = 2;
    public float attackCooldown = 1f; // Cooldown de ataque en segundos

    private void Awake()
    {
        _enemy = GetComponent<EnemyModel>();
        _view = GetComponent<EnemyView>();

        // Obtener referencia al jugador
        _player = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        actualHealth = maxHealth;
        _playerTransform = _player.transform; // Asignar el transform del jugador
    }

    private void Update()
    {
        if (actualHealth <= 0)
        {
            Die();
        }

        FollowPlayer(); // Llama a la función FollowPlayer en cada frame

        // Verificar si el jugador está dentro del rango de ataque y si el enemigo puede atacar
        if (Vector2.Distance(transform.position, _playerTransform.position) <= attackRange && canAttack)
        {
            Attack();
            StartCoroutine(AttackCooldown()); // Iniciar el cooldown del ataque
        }
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

    public void Attack()
    {
        _player.GetDamaged(damage);
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

    IEnumerator AttackCooldown()
    {
        canAttack = false; // Desactivar la capacidad de atacar
        yield return new WaitForSeconds(attackCooldown); // Esperar el tiempo de cooldown
        canAttack = true; // Activar la capacidad de atacar nuevamente
    }
}
