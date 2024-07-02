using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public PlayerView playerView;
    [SerializeField] public ClassManager playerClassManager;
    [SerializeField] public ExperienceManager experienceManager;
    [SerializeField] public Boomerang boomerang;

    public bool _isInCombat = false;
    public int attackType = 0;    
    
    public float _timeSinceLastRangeAttack = 0f;
    public float _timeSinceLastSlashAttack = 0f;
    public float _timeSinceLastOrbitalWeaponAttack = 0f;
    public float _timeSinceLastBoomerangAttack = 0f;
    public float _timeSinceLastCurveSwordAttack = 0f;
    float _swordSlashDuration = 0.1f;

    bool _rangedWeaponInCooldown = true;
    bool _slashAttackInCooldown = true;
    bool _orbitalWeaponInCooldown = true;
    bool _boomerangWeaponInCooldown = true;
    bool _curveSwordInCooldown = true;

    public GameObject basicSlash;
    public GameObject orbitalWeapon;
    public GameObject kunaiPrefab;
    public GameObject boomerangPrefab;
    public GameObject curveSword;

    public Action OnAttack;
    private Dictionary<int, System.Action> _attackDictionary = new Dictionary<int, System.Action>();

    public bool _meleeCanAttack = false;
    public bool _rangedCanAttack = false;
    public bool _engineerCanAttack = false;
    public bool _boomerangCanAttack = false;
    public bool _curveSwordCanAttack = false;

    private void Awake()
    {
        _attackDictionary.Add(1, BasicSlash);
        _attackDictionary.Add(2, OrbitalWeapon);
        _attackDictionary.Add(3, CurveSword);
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
        if(experienceManager != null)
        {
            if (_slashAttackInCooldown)
            {
                _timeSinceLastSlashAttack += Time.deltaTime;
                if (_timeSinceLastSlashAttack >= experienceManager._meleeCooldown)
                {
                    _slashAttackInCooldown = false;
                }
            }

            if (_orbitalWeaponInCooldown)
            {
                _timeSinceLastOrbitalWeaponAttack += Time.deltaTime;
                if (_timeSinceLastOrbitalWeaponAttack >= experienceManager._orbitalCooldown && _timeSinceLastOrbitalWeaponAttack >= experienceManager._orbitalDuration)
                {
                    _orbitalWeaponInCooldown = false;
                }
            }

            if (_curveSwordInCooldown)
            {
                _timeSinceLastCurveSwordAttack += Time.deltaTime;
                if (_timeSinceLastCurveSwordAttack >= experienceManager._curveSwordCooldown && _timeSinceLastCurveSwordAttack >= experienceManager._curveSwordDuration)
                {
                    _curveSwordInCooldown = false;
                }
            }

            if (_rangedWeaponInCooldown && _isInCombat)
            {
                _timeSinceLastRangeAttack += Time.deltaTime;
                if (_timeSinceLastRangeAttack >= experienceManager._rangedCooldown && _rangedCanAttack)
                {
                    _rangedWeaponInCooldown = false;
                    playerView.Attack(true);
                    RangeAttack();
                }
            }

            if (_boomerangWeaponInCooldown && _isInCombat)
            {
                _timeSinceLastBoomerangAttack += Time.deltaTime;
                if (_timeSinceLastBoomerangAttack >= experienceManager._boomerangCooldown && _boomerangCanAttack)
                {
                    _boomerangWeaponInCooldown = false;
                    playerView.Attack(true);
                    BoomerangAttack();
                }
            }
        }

        
    }

    public void WeaponSelector()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && attackType != 1 && _meleeCanAttack)
        {
               attackType = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && attackType != 2 && _engineerCanAttack)
        {
            attackType = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && attackType != 3 && _curveSwordCanAttack)
        {
            attackType = 3;
        }
    }

    public void UseWeapon()
    {
        if (Input.GetMouseButtonDown(0) && attackType != 0)
        {
            if (_attackDictionary.ContainsKey(attackType))
            {
                _attackDictionary[attackType]?.Invoke();
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
            Invoke("DeactivateOrbitalWeapon", experienceManager._orbitalDuration);
        }        
    }

    private void DeactivateOrbitalWeapon()
    {
        orbitalWeapon.SetActive(false);
    }

    private void CurveSword()
    {
        if (!_curveSwordInCooldown)
        {
            curveSword.SetActive(true);
            _timeSinceLastCurveSwordAttack = 0f;
            _curveSwordInCooldown = true;
            Invoke("DeactivateCurveSword", experienceManager._curveSwordDuration);
        }
    }

    private void DeactivateCurveSword()
    {
        curveSword.SetActive(false) ;
    }
    
    private void RangeAttack()
    {
        _rangedWeaponInCooldown = true;
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

    private void BoomerangAttack()
    {
        float angle = transform.localScale.x > 0 ? 0f : 180f;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Instantiate(boomerangPrefab, transform.position, rotation);

        _boomerangWeaponInCooldown = true;
        _timeSinceLastBoomerangAttack = 0f;
        playerView.Attack(false);
    }

}
