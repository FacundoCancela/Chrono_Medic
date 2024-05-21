
using UnityEngine;

[CreateAssetMenu(fileName = "ActorStats", menuName = "ScriptableObjects/ActorStats", order = 1)]
public class PlayerStats : ScriptableObject
{

    //Player stats
    public int maxHealth = 100;
    public int damageMultiplier = 1;
    public int money = 0;

    //WeaponStats

    public int swordDamage = 2;
    public int projectileDamage = 1;       
    
}