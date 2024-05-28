using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] public PlayerStats playerStats;

    public int attackType = 0;

    public float _weaponCooldown = 1f;
    public float _timeSinceLastMeleeAttack = 0f;
    public float _timeSinceLastRangeAttack = 0f;
    float _swordSlashDuration = 0.1f;
    float _orbitalDuration = 5f;

    bool _meleeAttackInCooldown = true;
    bool _rangeAttackInCooldown = true;
    public bool _isInCombat = false;

    public GameObject basicSlash;
    public GameObject bigSlash;
    public GameObject orbitalWeapon;
    public GameObject kunaiPrefab;

    private Dictionary<int, System.Action> _attackDictionary = new Dictionary<int, System.Action>();

    private void Awake()
    {
        _attackDictionary.Add(1, BasicSlash);
        _attackDictionary.Add(2, BigSlash);
        _attackDictionary.Add(3, OrbitalWeapon);
    }

    private void Update()
    {
        WeaponCooldown();
        WeaponSelector();
        UseWeapon();
    }

    public void WeaponCooldown()
    {
        // Si no estamos atacando, actualizar el tiempo desde el último ataque
        if (_meleeAttackInCooldown)
        {
            _timeSinceLastMeleeAttack += Time.deltaTime;
            if (_timeSinceLastMeleeAttack >= _weaponCooldown)
            {
                _meleeAttackInCooldown = false;
            }
        }

        if (_rangeAttackInCooldown && _isInCombat)
        {
            _timeSinceLastRangeAttack += Time.deltaTime;
            if (_timeSinceLastRangeAttack >= _weaponCooldown)
            {
                _rangeAttackInCooldown = false;
                RangeAttack();
            }
        }
    }

    public void WeaponSelector()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && attackType != 1)
        {
            if(playerStats.basicSlashUnlocked)
            {
                attackType = 1;
            }
            else
            {
                Debug.Log("arma no desbloqueada");
                attackType = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && attackType != 2)
        {
            if(playerStats.bigSlashUnlocked)
            {
                attackType = 2;
            }
            else
            {
                Debug.Log("arma no desbloqueada");
                attackType = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && attackType != 3)
        {
            if(playerStats.orbitalWeaponUnlocked)
            {
                attackType = 3;
            }
            else
            {
                Debug.Log("arma no desbloqueada");
                attackType = 0;
            }
        }
    }

    public void UseWeapon()
    {
        if (Input.GetKeyDown(KeyCode.F) && !_meleeAttackInCooldown && attackType != 0)
        {
            _meleeAttackInCooldown = true;
            _timeSinceLastMeleeAttack = 0f;
            if (_attackDictionary.ContainsKey(attackType))
            {
                _attackDictionary[attackType]?.Invoke();
            }
        }
    }

    private void BasicSlash()
    {

        basicSlash.SetActive(true);
        Invoke("DeactivateBasicSlash", _swordSlashDuration);
    }

    private void DeactivateBasicSlash()
    {
        basicSlash.SetActive(false);
    }

    private void OrbitalWeapon()
    {

        orbitalWeapon.SetActive(true);
        Invoke("DeactivateOrbitalWeapon", _orbitalDuration);
    }

    private void DeactivateOrbitalWeapon()
    {
        orbitalWeapon.SetActive(false);
    }
    private void BigSlash()
    {

        bigSlash.SetActive(true);
        Invoke("DeactivateBigSlash", _swordSlashDuration);
    }

    private void DeactivateBigSlash()
    {
        bigSlash.SetActive(false);
    }

    private void RangeAttack()
    {
        _rangeAttackInCooldown = true;
        _timeSinceLastRangeAttack = 0f;

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
