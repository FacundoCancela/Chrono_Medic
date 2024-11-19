using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveAttack : MonoBehaviour, IWeapon
{
    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public ExperienceManager experienceManager;
    [SerializeField] public GameObject curveSword;
    [SerializeField] public Transform attackPosition;
    [SerializeField] public float _timeSinceLastCurveAttack = 0f;
    public bool _curveAttackInCooldown = false;

    private void Update()
    {
        if (_curveAttackInCooldown)
        {
            _timeSinceLastCurveAttack -= Time.deltaTime;
        }
        if (experienceManager != null)
        {
            if (_timeSinceLastCurveAttack <= 0f)
            {
                _curveAttackInCooldown = false;
                _timeSinceLastCurveAttack = experienceManager.curveSwordCooldown + experienceManager.curveSwordDuration;
            }
        }
        
    }

    public void Attack()
    {
        if (!_curveAttackInCooldown)
        {
            Instantiate(curveSword, attackPosition);
            _curveAttackInCooldown = true;
        }
    }
}
