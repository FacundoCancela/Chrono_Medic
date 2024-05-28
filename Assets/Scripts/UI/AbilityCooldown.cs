using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour
{
    public enum AttackType
    {
        Slash,
        BigSlash,
        OrbitalWeapon,
        Ranged
    }

    [SerializeField] public Image cooldownImage;
    [SerializeField] public WeaponManager weaponManager;
    [SerializeField] public AttackType attackType;

    private void Update()
    {
        if (weaponManager._isInCombat)
        {
            switch (attackType)
            {
                case AttackType.Slash:
                    cooldownImage.fillAmount = weaponManager._timeSinceLastSlashAttack;
                    break;
                case AttackType.BigSlash:
                    cooldownImage.fillAmount = weaponManager._timeSinceLastBigSlashAttack
                        ; break;
                case AttackType.OrbitalWeapon:
                    cooldownImage.fillAmount = weaponManager._timeSinceLastOrbitalWeaponAttack
                        ; break;
                case AttackType.Ranged:
                    cooldownImage.fillAmount = weaponManager._timeSinceLastRangeAttack;
                    break;
                default:
                    break;
            }
        }
    }
}
