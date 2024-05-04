using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    IActorModel _player;
    IActorView _view;

    float _weaponCooldown = 1f;
    float _timeSinceLastAttack = 0f;
    float _swordSlashDuration = 0.1f;
    bool _attackInCooldown = false;
    public GameObject sword;
    public GameObject kunaiPrefab;

    public int attackType = 1;

    public string nombreEscenaAJugar;

    public int maxHealth = 10;
    public int actualHealth;

    private void Awake()
    {
        _player = GetComponent<IActorModel>();
        _view = GetComponent<IActorView>();
    }

    private void Start()
    {
        actualHealth = maxHealth;
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y).normalized;
        _player.Move(dir);
        _view.LookDir(dir);

        // Si no estamos atacando, actualizar el tiempo desde el último ataque
        if (_attackInCooldown)
        {
            _timeSinceLastAttack += Time.deltaTime;
            if( _timeSinceLastAttack >= _weaponCooldown )
            {
                _attackInCooldown = false;
            }
        }

        //Control de ataque
        if(Input.GetKeyDown(KeyCode.F) && !_attackInCooldown)
        {
            _attackInCooldown = true;
            _timeSinceLastAttack = 0f;

            if (attackType == 1)
            {
                MeleeAttack();
            }
            else if(attackType == 2)
            {
                RangeAttack();
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            attackType = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            attackType = 2;
        }

        if (actualHealth <= 0)
        {
            Die();
        }
    }

    public void GetDamaged(int damage)
    {
        _view.GetDamaged();
        actualHealth -= damage;
    }

    public void Die()
    {
        SceneManager.LoadScene(nombreEscenaAJugar);
    }

    private void MeleeAttack()
    {
        
        sword.SetActive(true);
        Invoke("DeactivateSword", _swordSlashDuration);
    }

    private void DeactivateSword()
    {
        sword.SetActive(false);
    }

    private void RangeAttack()
    {
        _attackInCooldown = true;
        _timeSinceLastAttack = 0f;

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
    }
}
