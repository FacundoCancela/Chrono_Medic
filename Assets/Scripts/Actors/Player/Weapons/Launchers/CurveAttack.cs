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
    bool _curveAttackInCooldown = false;

    private void Update()
    {
        if (_curveAttackInCooldown)
        {
            _timeSinceLastCurveAttack += Time.deltaTime;
        }
        if (experienceManager != null)
        {
            if (_timeSinceLastCurveAttack > experienceManager.curveSwordCooldown)
            {
                _curveAttackInCooldown = false;
                _timeSinceLastCurveAttack = 0f;
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
