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
    bool _orbitalAttackInCooldown = false;

    // Controla si est� en modo especial
    public bool specialAttackMode = false;

    private void Update()
    {
        if (_orbitalAttackInCooldown)
        {
            _timeSinceLastOrbitalAttack += Time.deltaTime;
        }
        if (experienceManager != null)
        {
            if (_timeSinceLastOrbitalAttack > experienceManager.orbitalCooldown + experienceManager.orbitalDuration)
            {
                _orbitalAttackInCooldown = false;
                _timeSinceLastOrbitalAttack = 0f;
            }

        }

    }

    public void Attack()
    {
        if (!_orbitalAttackInCooldown)
        {
            // Si est� en modo especial, el rango y tama�o son el doble
            float rangeMultiplier = specialAttackMode ? 2f : 1f;
            float sizeMultiplier = specialAttackMode ? 2f : 1f;

            float angleStep = 360f / experienceManager.numberOfOrbs;
            float startingAngle = 0f;

            for (int i = 0; i < experienceManager.numberOfOrbs; i++)
            {
                float angle = startingAngle + i * angleStep;
                Vector3 orbPosition = GetPositionAtAngle(attackPosition.position, angle, experienceManager.orbitalRange * rangeMultiplier);
                GameObject orb = Instantiate(Orbe, orbPosition, Quaternion.identity);

                // Ajustamos el tama�o del orbe
                orb.transform.localScale *= sizeMultiplier;

                // Asigna el �ngulo inicial al orbe instanciado
                Orbe orbeScript = orb.GetComponent<Orbe>();
                if (orbeScript != null)
                {
                    orbeScript.SetInitialAngle(angle * Mathf.Deg2Rad);
                }
            }
            if(specialAttackMode)
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
