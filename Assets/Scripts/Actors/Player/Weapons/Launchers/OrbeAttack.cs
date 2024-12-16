using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbeAttack : MonoBehaviour, IWeapon
{
    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public ExperienceManager experienceManager;
    [SerializeField] public GameObject Orbe;
    [SerializeField] public Transform attackPosition;
    [SerializeField] public float _timeSinceLastOrbitalAttack = 0f;
    public bool _orbitalAttackInCooldown = false;

    
    public bool specialAttackMode = false;

    private void Update()
    {
        if (_orbitalAttackInCooldown)
        {
            _timeSinceLastOrbitalAttack -= Time.deltaTime;
        }
        if (experienceManager != null)
        {
            if (_timeSinceLastOrbitalAttack <= 0f)
            {
                _orbitalAttackInCooldown = false;
                _timeSinceLastOrbitalAttack = experienceManager.orbitalCooldown + experienceManager.orbitalDuration;
            }

        }

    }

    public void Attack()
    {
        if (!_orbitalAttackInCooldown)
        {
            float angleStep = 360f / experienceManager.numberOfOrbs;
            float startingAngle = 0f;

            for (int i = 0; i < experienceManager.numberOfOrbs; i++)
            {
                float angle = startingAngle + i * angleStep;

                
                Vector3 orbPosition = GetPositionAtAngle(attackPosition.position, angle, experienceManager.orbitalRange);
                GameObject orb = Instantiate(Orbe, orbPosition, Quaternion.identity);
                Orbe orbeScript = orb.GetComponent<Orbe>();
                if (orbeScript != null)
                {
                    orbeScript.SetInitialAngle(angle * Mathf.Deg2Rad);
                }

                
                if (specialAttackMode)
                {
                    Vector3 specialOrbPosition = GetPositionAtAngle(attackPosition.position, angle, experienceManager.orbitalRange * 2f);
                    GameObject specialOrb = Instantiate(Orbe, specialOrbPosition, Quaternion.identity);

                   
                    specialOrb.transform.localScale *= 2f;

                    Orbe specialOrbeScript = specialOrb.GetComponent<Orbe>();
                    if (specialOrbeScript != null)
                    {
                        specialOrbeScript.SetInitialAngle(angle * Mathf.Deg2Rad);
                    }
                }
            }

            if (specialAttackMode)
                specialAttackMode = false;

            _orbitalAttackInCooldown = true;
        }
    }

    private Vector3 GetPositionAtAngle(Vector3 center, float angleDegrees, float radius)
    {
        float angleRadians = angleDegrees * Mathf.Deg2Rad;
        float x = center.x + Mathf.Cos(angleRadians) * radius;
        float y = center.y + Mathf.Sin(angleRadians) * radius;
        return new Vector3(x, y, center.z);
    }
}
