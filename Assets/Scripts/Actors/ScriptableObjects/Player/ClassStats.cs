using UnityEngine;

[CreateAssetMenu(fileName = "ClassStats", menuName = "ScriptableObjects/ClassStats", order = 1)]
public class ClassStats : ScriptableObject
{
    public int maxHealth = 100;
    public int damageMultiplier = 1;
    public int movementSpeed = 1;
    public float attackSpeed = 1;

    public ClassManager.SelectedClass selectedClass; 
}