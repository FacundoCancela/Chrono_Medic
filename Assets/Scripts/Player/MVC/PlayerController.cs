using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    IPlayerModel _player;
    IPlayerView _view;

    float _weaponCooldown = 1f;
    float _timeSinceLastAttack = 0f;
    bool _isAttacking = false;
    public GameObject sword;
    public GameObject kunaiPrefab;

    private void Awake()
    {
        _player = GetComponent<IPlayerModel>();
        _view = GetComponent<IPlayerView>();
    }

    private void Start()
    {
        Attack();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y).normalized;
        _player.Move(dir);
        _view.LookDir(dir);

        // Si no estamos atacando, actualizar el tiempo desde el último ataque
        if (!_isAttacking)
        {
            _timeSinceLastAttack += Time.deltaTime;
        }

        // Si no estamos atacando y ha pasado el cooldown, entonces atacar
        if (!_isAttacking && _timeSinceLastAttack >= _weaponCooldown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        MeleeAttack();
        RangeAttack();
    }

    private void MeleeAttack()
    {
        _isAttacking = true;
        _timeSinceLastAttack = 0f;
        sword.SetActive(true);
        StartCoroutine(DeactivateSwordAfterDelay(0.1f));
    }

    private void RangeAttack()
    {
        _isAttacking = true;

        // Obtener todos los objetos EnemyController en la escena
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();

        // Inicializar variables para el enemigo más cercano
        EnemyController closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        // Iterar sobre cada enemigo para encontrar el más cercano
        foreach (EnemyController enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        // Si se encontró un enemigo, lanzar el Kunai hacia él
        if (closestEnemy != null)
        {
            Vector2 direction = (closestEnemy.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            GameObject kunai = Instantiate(kunaiPrefab, transform.position, rotation);
            kunai.GetComponent<Rigidbody2D>().velocity = direction * 10f; // Velocidad del Kunai
        }

        // Finalizar el ataque después de un tiempo de espera
        StartCoroutine(EndRangeAttackAfterDelay(0.1f));
    }


    private IEnumerator EndRangeAttackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _isAttacking = false;
    }

    private IEnumerator DeactivateSwordAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        sword.SetActive(false);
        _isAttacking = false;
    }
}
