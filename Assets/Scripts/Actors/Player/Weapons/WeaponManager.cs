using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] public List<IWeapon> _automaticWeapons = new List<IWeapon>();

    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public PlayerView playerView;
    [SerializeField] public ClassManager playerClassManager;
    [SerializeField] public ExperienceManager experienceManager;
    [SerializeField] public PlayerController playerController;

    public bool _isInCombat = false;
    
    public bool _meleeCanAttack = false;
    public bool _rangedCanAttack = false;
    public bool _engineerCanAttack = false;
    public bool _boomerangCanAttack = false;
    public bool _curveSwordCanAttack = false;

    private float _timeSinceLastSpecial = 0f;
    private bool _canUseSpecial = true;

    private IWeapon currentClassWeapon;



    private void Awake()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Lvl_Menu")
        {
            _isInCombat = false;
        }
        else
        {
            _isInCombat = true;
        }

        ActivateWeaponClass();
    }

    private void Update()
    {
        if(_isInCombat && playerController.playerAlive && playerController.playerControllable)
        {
            UseWeapon();
        }

        if (!_canUseSpecial)
        {
            _timeSinceLastSpecial -= Time.deltaTime;
            if (_timeSinceLastSpecial <= 0)
            {
                _canUseSpecial = true; // Se puede volver a activar
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && _canUseSpecial && _isInCombat)
        {
            StartCoroutine(ActivateSpecialMode());
        }
    }

    private void ActivateWeaponClass()
    {
        switch (ClassManager.currentClass)
        {
            case ClassManager.SelectedClass.Melee:
                _meleeCanAttack = true;
                currentClassWeapon = FindAnyObjectByType<SwordAttack>();
                AddAutomaticWeapon(currentClassWeapon);
                break;
            case ClassManager.SelectedClass.Ranged:
                _rangedCanAttack = true;
                currentClassWeapon = FindAnyObjectByType<RangedAttack>();
                AddAutomaticWeapon(currentClassWeapon);
                break;
            case ClassManager.SelectedClass.Engineer:
                _engineerCanAttack = true;
                currentClassWeapon = FindAnyObjectByType<OrbeAttack>();
                AddAutomaticWeapon(currentClassWeapon);
                break;
        }
    }

    public void AddAutomaticWeapon(IWeapon weapon)
    {
        if (!_automaticWeapons.Contains(weapon))
        {
            _automaticWeapons.Add(weapon);
        }
    }

    public void UseWeapon()
    {
        foreach (IWeapon autoWeapon in _automaticWeapons)
        {
            autoWeapon.Attack();
        }
    }

    private IEnumerator ActivateSpecialMode()
    {
        Debug.Log("ulti activada, duracion: " + playerStats.ultimateDuration);
        if (currentClassWeapon is RangedAttack rangedWeapon && experienceManager.actualRangedLevel == experienceManager.maxUpgradeableLevel)
        {
            rangedWeapon.specialAttackMode = true;
        }

        else if (currentClassWeapon is OrbeAttack orbeWeapon && experienceManager.actualEngineerLevel == experienceManager.maxUpgradeableLevel)
        {
            orbeWeapon.specialAttackMode = true;  // Activamos modo especial en orbes
        }

        else if (currentClassWeapon is SwordAttack swordWeapon && experienceManager.actualMeleeLevel == experienceManager.maxUpgradeableLevel)
        {
            swordWeapon.specialAttackMode = true;
        }

        yield return new WaitForSeconds(playerStats.ultimateDuration);

        Debug.Log("ulti desactivada, cooldown: " + playerStats.ultimateCooldown);

        if (currentClassWeapon is RangedAttack rangedWeapon2)
        {
            rangedWeapon2.specialAttackMode = false;
        }
        else if (currentClassWeapon is SwordAttack swordWeapon2)
        {
            swordWeapon2.specialAttackMode = false;
        }

        _canUseSpecial = false;
        _timeSinceLastSpecial = playerStats.ultimateCooldown;
    }
}
