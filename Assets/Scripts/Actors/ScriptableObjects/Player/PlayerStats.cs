using UnityEngine;

[CreateAssetMenu(fileName = "ActorStats", menuName = "ScriptableObjects/ActorStats", order = 1)]
public class PlayerStats : ScriptableObject
{

    //Player stats
    public int money = 0;
    public int maxHealth = 100;
    public float maxBuyHealth = 100;
    public int damageMultiplier = 1;
    public int movementSpeed = 1;
    public int actualLevel;
    public float attackSpeed = 1;
    public float attackRange;

    //Shop prices
    public int upgradeCost = 50;

    //Items
    public int InjectionHeal = 15;
    public int maxInjectionsHeal = 30;
    public int InjectionsLimit = 5;
    public int maxInjectionsLimit = 10;

}