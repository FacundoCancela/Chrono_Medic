using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUnlocked : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] public List<GameObject> abilityObjects;
    [SerializeField] private List<Sprite> abilitySprites;

    private HashSet<int> activatedSprites = new HashSet<int>();

    private int currentAbilityIndex = 0;


    void Update()
    {
        UpdateAbilities();
    }

    public void UpdateAbilities()
    {
        if (weaponManager._rangedCanAttack && currentAbilityIndex < abilityObjects.Count && !activatedSprites.Contains(0))
        {
            SetAbilitySprite(currentAbilityIndex, abilitySprites[0]);
            activatedSprites.Add(0);
            currentAbilityIndex++;
        }

        if (weaponManager._meleeCanAttack && currentAbilityIndex < abilityObjects.Count && !activatedSprites.Contains(1))
        {
            SetAbilitySprite(currentAbilityIndex, abilitySprites[1]);
            activatedSprites.Add(1);
            currentAbilityIndex++;
        }

        if (weaponManager._engineerCanAttack && currentAbilityIndex < abilityObjects.Count && !activatedSprites.Contains(2))
        {
            SetAbilitySprite(currentAbilityIndex, abilitySprites[2]);
            activatedSprites.Add(2);
            currentAbilityIndex++;
        }

        if (weaponManager._boomerangCanAttack && currentAbilityIndex < abilityObjects.Count && !activatedSprites.Contains(3))
        {
            SetAbilitySprite(currentAbilityIndex, abilitySprites[3]);
            activatedSprites.Add(3);
            currentAbilityIndex++;
        }

        if (weaponManager._curveSwordCanAttack && currentAbilityIndex < abilityObjects.Count && !activatedSprites.Contains(4))
        {
            SetAbilitySprite(currentAbilityIndex, abilitySprites[4]);
            activatedSprites.Add(4);
            currentAbilityIndex++;
        }

        // Desactivar los objetos restantes
        for (int i = currentAbilityIndex; i < abilityObjects.Count; i++)
        {
            abilityObjects[i].SetActive(false);
        }
    }


    void SetAbilitySprite(int index, Sprite sprite)
    {
        abilityObjects[index].SetActive(true);
        abilityObjects[index].GetComponent<Image>().sprite = sprite;
    }
}