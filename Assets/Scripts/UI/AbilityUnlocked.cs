using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUnlocked : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] public List<GameObject> abilityObjects;
    [SerializeField] private List<AbilityData> abilities;

    private HashSet<string> activatedAbilities = new HashSet<string>();

    private int currentAbilityIndex = 0;

    void Update()
    {
        UpdateAbilities();
    }

    public void UpdateAbilities()
    {
        foreach (var ability in abilities)
        {
            if (IsAbilityUnlocked(ability.abilityName) && !activatedAbilities.Contains(ability.abilityName))
            {
                if (currentAbilityIndex < abilityObjects.Count)
                {
                    SetAbilitySprite(currentAbilityIndex, ability.abilitySprite);
                    activatedAbilities.Add(ability.abilityName);
                    currentAbilityIndex++;
                }
            }
        }

        for (int i = currentAbilityIndex; i < abilityObjects.Count; i++)
        {
            abilityObjects[i].SetActive(false);
        }
    }

    private bool IsAbilityUnlocked(string abilityName)
    {
        return abilityName switch
        {
            "Ranged" => weaponManager._rangedCanAttack,
            "Melee" => weaponManager._meleeCanAttack,
            "Engineer" => weaponManager._engineerCanAttack,
            "Boomerang" => weaponManager._boomerangCanAttack,
            "CurveSword" => weaponManager._curveSwordCanAttack,
            _ => false,
        };
    }

    void SetAbilitySprite(int index, Sprite sprite)
    {
        abilityObjects[index].SetActive(true);
        abilityObjects[index].GetComponent<Image>().sprite = sprite;
    }
}

[System.Serializable]
public class AbilityData
{
    public string abilityName; 
    public Sprite abilitySprite;
}