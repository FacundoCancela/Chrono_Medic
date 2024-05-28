using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour
{
    [SerializeField] public Image cooldownImage;
    [SerializeField] public WeaponManager weaponManager;
    [SerializeField] public bool rangedAttack;
    [SerializeField] public bool meleeAttack;


    private void Update()
    {
       if(meleeAttack)
        {
            cooldownImage.fillAmount = weaponManager._timeSinceLastMeleeAttack;
        }

       if(rangedAttack)
        {
            cooldownImage.fillAmount = weaponManager._timeSinceLastRangeAttack;
        }

    }


}
