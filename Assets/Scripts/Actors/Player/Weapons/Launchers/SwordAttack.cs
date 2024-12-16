using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour, IWeapon
{
    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public ExperienceManager experienceManager;
    [SerializeField] public GameObject swordSlash;
    [SerializeField] public GameObject specialSwordSlash;
    [SerializeField] public Transform attackPosition;
    [SerializeField] public float _timeSinceLastSlashAttack = 0f;
    public bool _slashAttackInCooldown = false;

    public bool specialAttackMode = false;

    private void Update()
    {
        if (_slashAttackInCooldown)
        {
            _timeSinceLastSlashAttack -= Time.deltaTime;
        }
        if (experienceManager != null)
        {
            if (_timeSinceLastSlashAttack <= 0f)
            {
                _slashAttackInCooldown = false;
                _timeSinceLastSlashAttack = experienceManager.meleeCooldown;
            }
        }
    }

    public void Attack()
    {
        if (!_slashAttackInCooldown)
        {
            GameObject selectedPrefab = specialAttackMode ? specialSwordSlash : swordSlash;
            GameObject attackInstance = Instantiate(selectedPrefab, attackPosition.position, Quaternion.identity);

            
            Vector3 attackDirection = transform.parent.localScale.x > 0 ? Vector3.right : Vector3.left;
            attackInstance.transform.right = attackDirection;

            _slashAttackInCooldown = true;
        }
    }
}
