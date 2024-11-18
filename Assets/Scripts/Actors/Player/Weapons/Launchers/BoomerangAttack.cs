using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangAttack : MonoBehaviour, IWeapon
{
    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public ExperienceManager experienceManager;
    [SerializeField] public GameObject boomerang;
    [SerializeField] public Transform attackPosition;
    [SerializeField] public float _timeSinceLastBoomerangAttack = 0f;
    bool _boomerangAttackInCooldown = false;

    private void Update()
    {
        if (_boomerangAttackInCooldown)
        {
            _timeSinceLastBoomerangAttack -= Time.deltaTime;
        }
        if (experienceManager != null)
        {
            if (_timeSinceLastBoomerangAttack <= 0f)
            {
                _boomerangAttackInCooldown = false;
                _timeSinceLastBoomerangAttack = experienceManager.boomerangCooldown;
            }

        }

    }

    public void Attack()
    {
        if (!_boomerangAttackInCooldown)
        {
            float angle = transform.localScale.x > 0 ? 0f : 180f;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Instantiate(boomerang, transform.position, rotation);
            _boomerangAttackInCooldown = true;
        }
    }

}
