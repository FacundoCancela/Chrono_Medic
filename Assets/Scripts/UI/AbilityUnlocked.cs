using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUnlocked : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private List<GameObject> abilityObjects;

    void Update()
    {
        for (int i = 0; i < abilityObjects.Count; i++)
        {
            switch (i)
            {
                case 0:
                    abilityObjects[i].SetActive(weaponManager._rangedCanAttack);
                    break;
                case 1:
                    abilityObjects[i].SetActive(weaponManager._meleeCanAttack);
                    break;
                case 2:
                    abilityObjects[i].SetActive(weaponManager._engineerCanAttack);
                    break;
                case 3:
                    abilityObjects[i].SetActive(weaponManager._boomerangCanAttack); 
                    break;
                case 4:
                    abilityObjects[i].SetActive(weaponManager._curveSwordCanAttack); 
                    break;
                default:
                    break;
            }
        }

    }
}
