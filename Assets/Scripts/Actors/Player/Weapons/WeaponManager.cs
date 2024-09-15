using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] public List<IWeapon> _manualWeapons = new List<IWeapon>();
    [SerializeField] public List<IWeapon> _automaticWeapons = new List<IWeapon>();
    [SerializeField] public int selectedWeapon = 1;

    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public PlayerView playerView;
    [SerializeField] public ClassManager playerClassManager;
    [SerializeField] public ExperienceManager experienceManager;

    public bool _isInCombat = false;
    
    public bool _meleeCanAttack = false;
    public bool _rangedCanAttack = false;
    public bool _engineerCanAttack = false;
    public bool _boomerangCanAttack = false;
    public bool _curveSwordCanAttack = false;

    private void Awake()
    {
        // Check current scene based on name
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
        if(_isInCombat)
        {
            WeaponSelector();
            UseWeapon();
        }
    }

    private void ActivateWeaponClass()
    {
        Debug.Log(ClassManager.currentClass);
        switch (ClassManager.currentClass)
        {
            case ClassManager.SelectedClass.Melee:
                _meleeCanAttack = true;
                IWeapon sword = FindAnyObjectByType<SwordAttack>();
                AddManualWeapon(sword);
                if(experienceManager != null)experienceManager.MeleeLevelUp();
                break;
            case ClassManager.SelectedClass.Ranged:
                _rangedCanAttack = true;
                IWeapon ranged = FindAnyObjectByType<RangedAttack>();
                AddAutomaticWeapon(ranged);
                if (experienceManager != null)experienceManager.RangedLevelUp();
                break;
            case ClassManager.SelectedClass.Engineer
                : _engineerCanAttack = true;
                IWeapon orbe = FindAnyObjectByType<OrbeAttack>();
                AddManualWeapon(orbe);
                if (experienceManager != null)experienceManager.EngineerLevelUp();
                break;
        }
    }

    public void AddManualWeapon(IWeapon weapon)
    {
        if (!_manualWeapons.Contains(weapon))
        {
            _manualWeapons.Add(weapon);
        }
    }

    public void AddAutomaticWeapon(IWeapon weapon)
    {
        if (!_automaticWeapons.Contains(weapon))
        {
            _automaticWeapons.Add(weapon);
            Debug.Log("arma añadidida" + weapon);
        }
    }

    public void WeaponSelector()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedWeapon = 3;
        }
    }

    public void UseWeapon()
    {
        if (Input.GetMouseButtonDown(0) && selectedWeapon <= _manualWeapons.Count)
        {
            _manualWeapons[selectedWeapon-1].Attack();
        }
        foreach (IWeapon autoWeapon in _automaticWeapons)
        {
            autoWeapon.Attack();
        }
    }
    
}
