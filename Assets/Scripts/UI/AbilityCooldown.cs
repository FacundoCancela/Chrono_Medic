using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour
{
    public enum AttackType
    {
        Slash,
        OrbitalWeapon,
        Ranged,
        Boomerang,
        CurveSword,
    }

    [SerializeField] public Image cooldownImage;
    [SerializeField] public WeaponManager weaponManager;
    [SerializeField] public AttackType attackType;

    private void Update()
    {
        if (weaponManager._isInCombat)
        {
            //switch (attackType)
            //{
            //    case AttackType.Slash:
            //        cooldownImage.fillAmount = weaponManager._timeSinceLastSlashAttack / weaponManager._meleeCooldown;
            //        break;
            //    case AttackType.OrbitalWeapon:
            //        cooldownImage.fillAmount = weaponManager._timeSinceLastOrbitalWeaponAttack / weaponManager._meleeCooldown;
            //            ; break;
            //    case AttackType.Ranged:
            //        cooldownImage.fillAmount = weaponManager._timeSinceLastRangeAttack / weaponManager._meleeCooldown;
            //        break;
            //    case AttackType.Boomerang:
            //        cooldownImage.fillAmount = weaponManager._timeSinceLastBoomerangAttack/weaponManager._meleeCooldown;
            //        break;
            //    case AttackType.CurveSword:
            //        cooldownImage.fillAmount = weaponManager._timeSinceLastCurveSwordAttack/weaponManager._curveSwordCooldown;
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}
