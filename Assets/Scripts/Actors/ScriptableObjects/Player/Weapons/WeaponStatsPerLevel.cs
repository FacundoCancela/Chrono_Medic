using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatsPerLevel", menuName = "ScriptableObjects/WeaponStats", order = 1)]

public class WeaponStatsPerLevel : ScriptableObject
{
    public List<int> weaponDamage;
    public List<float> cooldown;
    public List<float> duration;
    public List<int> rotationSpeed;
    public List<int> rotationRange;
    public List<int> numberOfOrbs;
}
