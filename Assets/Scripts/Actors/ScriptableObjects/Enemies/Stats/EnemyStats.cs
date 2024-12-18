
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats", order = 1)]
public class EnemyStats : ScriptableObject
{
    public int maxHealth = 5;
    public int damage = 1;
    public float movementSpeed = 1;
    public float attackRange;
    public float attackCooldown = 3;
    public int experienceDropped = 10;
    public int moneyDroped = 10;
    public float accuracyOffset = 1;
    // Puedes agregar m�s estad�sticas aqu� si es necesario
}