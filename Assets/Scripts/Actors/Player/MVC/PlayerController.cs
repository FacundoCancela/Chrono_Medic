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
<<<<<<< Updated upstream
    bool _isAttacking = false;
    public GameObject sword;
=======
    float _swordSlashDuration = 0.1f;
    bool _attackInCooldown = false;
    public GameObject basicSlash;
    public GameObject circleSlash;
>>>>>>> Stashed changes
    public GameObject kunaiPrefab;

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
        Attack();
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
        if (!_isAttacking)
        {
            _timeSinceLastAttack += Time.deltaTime;
        }

        // Si no estamos atacando y ha pasado el cooldown, entonces atacar
        if (!_isAttacking && _timeSinceLastAttack >= _weaponCooldown)
        {
<<<<<<< Updated upstream
            Attack();
=======
            _attackInCooldown = true;
            _timeSinceLastAttack = 0f;

            if (attackType == 1)
            {
                BasicSlash();
            }
            else if(attackType == 2)
            {
                RangeAttack();
            }
            else if(attackType == 3)
            {
                CircleSlash();
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            attackType = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            attackType = 2;
>>>>>>> Stashed changes
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            attackType = 3;
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

<<<<<<< Updated upstream
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
=======
    private void BasicSlash()
    {
        
        basicSlash.SetActive(true);
        Invoke("DeactivateBasicSlash", _swordSlashDuration);
    }

    private void DeactivateBasicSlash()
    {
        basicSlash.SetActive(false);
    }
    
    private void CircleSlash()
    {
        
        circleSlash.SetActive(true);
        Invoke("DeactivateCircleSlash", _swordSlashDuration);
    }

    private void DeactivateCircleSlash()
    {
        circleSlash.SetActive(false);
>>>>>>> Stashed changes
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
