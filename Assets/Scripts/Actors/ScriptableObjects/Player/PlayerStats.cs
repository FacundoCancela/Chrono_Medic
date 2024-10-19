using UnityEngine;

[CreateAssetMenu(fileName = "ActorStats", menuName = "ScriptableObjects/ActorStats", order = 1)]
public class PlayerStats : ScriptableObject
{

    [Header("Player Stats")]
    public int money = 0;
    public int maxHealth = 100;
    public int damageMultiplier = 1;
    public int movementSpeed = 1;
    public int actualLevel;
    public float attackSpeed = 1;
    public float attackRange;

    [Header("Shop Prices")]
    public int upgradeCost = 50;

    [Header("Shop upgrades")]
    public int ActualInjectionHeal = 15;
    public int ExtraInjectionHeal = 15;
    public int maxInjectionsHeal = 30;
    public int ActualInjectionsLimit = 5;
    public int ExtraInjectionsLimit = 5;
    public int maxInjectionsLimit = 10;
    public int ExtraBuyHealth = 100;
    public float maxBuyHealth = 100;

}