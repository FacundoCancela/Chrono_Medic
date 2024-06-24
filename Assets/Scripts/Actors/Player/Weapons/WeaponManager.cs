using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public PlayerView playerView;
    [SerializeField] public ClassManager playerClassManager;

    public bool _isInCombat = false;
    public int attackType = 0;
    public float _weaponCooldown = 1f;
    
    public float _timeSinceLastRangeAttack = 0f;
    public float _timeSinceLastSlashAttack = 0f;
    public float _timeSinceLastBigSlashAttack = 0f;
    public float _timeSinceLastOrbitalWeaponAttack = 0f;

    float _swordSlashDuration = 0.1f;
    float _orbitalDuration = 5f;

    bool _rangeAttackInCooldown = true;
    bool _slashAttackInCooldown = true;
    bool _orbitalWeaponInCooldown = true;

    public GameObject basicSlash;
    public GameObject orbitalWeapon;
    public GameObject kunaiPrefab;

    public Action OnAttack;
    private Dictionary<int, System.Action> _attackDictionary = new Dictionary<int, System.Action>();

    public bool _meleeCanAttack = false;
    public bool _rangedCanAttack = false;
    public bool _engineerCanAttack = false;

    private void Awake()
    {
        _attackDictionary.Add(1, BasicSlash);
        _attackDictionary.Add(2, OrbitalWeapon);
    }

    private void Start()
    {
        ActivateWeaponClass();
    }

    private void Update()
    {
        WeaponCooldown();
        WeaponSelector();
        UseWeapon();
    }

    private void ActivateWeaponClass()
    {
        switch (ClassManager.currentClass)
        {
            case ClassManager.SelectedClass.Melee:
                _meleeCanAttack = true;
                break;
            case ClassManager.SelectedClass.Ranged:
                _rangedCanAttack = true;
                break;
            case ClassManager.SelectedClass.Engineer
                : _engineerCanAttack = true;
                break;
        }
    }

    public void WeaponCooldown()
    {
        // Si no estamos atacando, actualizar el tiempo desde el último ataque
        if (_slashAttackInCooldown)
        {
            _timeSinceLastSlashAttack += Time.deltaTime;
            if (_timeSinceLastSlashAttack >= _weaponCooldown)
            {
                _slashAttackInCooldown = false;
            }
        }

        if (_orbitalWeaponInCooldown)
        {
            _timeSinceLastOrbitalWeaponAttack += Time.deltaTime;
            if (_timeSinceLastOrbitalWeaponAttack >= _weaponCooldown)
            {
                _orbitalWeaponInCooldown = false;
            }
        }

        if (_rangeAttackInCooldown && _isInCombat)
        {
            _timeSinceLastRangeAttack += Time.deltaTime;
            if (_timeSinceLastRangeAttack >= _weaponCooldown && _rangedCanAttack)
            {
                _rangeAttackInCooldown = false;
                playerView.Attack(true);
                RangeAttack();
            }
        }
    }

    public void WeaponSelector()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && attackType != 1 && _meleeCanAttack)
        {
            if(playerStats.basicSlashUnlocked)
            {
                attackType = 1;
                Debug.Log("Slash seleccionado");
            }
            else
            {
                Debug.Log("arma no desbloqueada");
                attackType = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && attackType != 2 && _engineerCanAttack)
        {
            if(playerStats.orbitalWeaponUnlocked)
            {
                attackType = 2;
                Debug.Log("Orbital seleccionado");
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
        if (Input.GetKeyDown(KeyCode.F) && attackType != 0)
        {
            if (_attackDictionary.ContainsKey(attackType))
            {
                _attackDictionary[attackType]?.Invoke();

                //OnAttack?.Invoke();
            } 
        }
    }

    private void BasicSlash()
    {
        if(!_slashAttackInCooldown)
        {
            basicSlash.SetActive(true);
            _timeSinceLastSlashAttack = 0f;
            _slashAttackInCooldown = true;
            Invoke("DeactivateBasicSlash", _swordSlashDuration);
        }        
    }

    private void DeactivateBasicSlash()
    {
        basicSlash.SetActive(false);
    }

    private void OrbitalWeapon()
    {
        if(!_orbitalWeaponInCooldown)
        {
            orbitalWeapon.SetActive(true);
            _timeSinceLastOrbitalWeaponAttack = 0f;
            _orbitalWeaponInCooldown = true;
            Invoke("DeactivateOrbitalWeapon", _orbitalDuration);
        }        
    }

    private void DeactivateOrbitalWeapon()
    {
        orbitalWeapon.SetActive(false);
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
        playerView.Attack(false);
    }
}
