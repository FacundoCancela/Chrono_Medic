using UnityEngine;

[CreateAssetMenu(fileName = "InitialData", menuName = "ScriptableObjects/InitialData", order = 1)]

public class InitialGameData : ScriptableObject
{
    [Header ("Player Stats")]
    public int maxHealth;
    public int damageMultiplier;
    public int defensePercentage;
    public int money;
    public int movementSpeed;
    public int actualLevel;
    public float attackSpeed;
    public float attackRange;

    [Header("Shop Prices")]

    public int upgradeCost;

    [Header("Items stats")]

    public int InjectionHeal = 5;
    public int InjectionsLimit = 5;

}
