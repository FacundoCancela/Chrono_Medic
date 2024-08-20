using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour, IWeapon
{
    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public ExperienceManager experienceManager;
    [SerializeField] public GameObject swordSlash;
    [SerializeField] public Transform attackPosition;
    [SerializeField] public float _timeSinceLastSlashAttack = 0f;
    bool _slashAttackInCooldown = false;

    private void Update()
    {
        if (_slashAttackInCooldown)
        {
            _timeSinceLastSlashAttack += Time.deltaTime;
        }
        if(experienceManager != null)
        {
            if (_timeSinceLastSlashAttack > experienceManager._meleeCooldown)
            {
                _slashAttackInCooldown = false;
                _timeSinceLastSlashAttack = 0f;
            }
        }
    }

    public void Attack()
    {
        if (!_slashAttackInCooldown)
        {
            Instantiate(swordSlash, attackPosition.position, Quaternion.identity);
            _slashAttackInCooldown = true;
        }
    }
}
