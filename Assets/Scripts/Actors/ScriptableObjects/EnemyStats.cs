
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats", order = 1)]
public class EnemyStats : ScriptableObject
{
    public int maxHealth = 5;
    public int damage = 1;
    public float shootRange;
    public float attackCooldown = 3;
    public float xpDropped = 10;
    public int moneyDroped = 10;
    // Puedes agregar más estadísticas aquí si es necesario
}