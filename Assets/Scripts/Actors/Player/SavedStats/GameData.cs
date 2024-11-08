using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Player stats
    public int maxHealth;
    public int damageMultiplier;
    public int defensePercentage;
    public int money;
    public int movementSpeed;
    public int actualLevel;
    public float attackSpeed;
    public float attackRange;

    //Shop prices
    public int upgradeCost;

    //Items
    public int InjectionHeal = 5;
    public int InjectionsLimit = 5;

    //Game
    public bool GameStarted = false;

}
