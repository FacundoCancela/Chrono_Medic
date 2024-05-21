
using UnityEngine;

[CreateAssetMenu(fileName = "ActorStats", menuName = "ScriptableObjects/ActorStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public int maxHealth = 100;
    public int damage = 1;
    public int money = 0;
        
    // Puedes agregar más estadísticas aquí si es necesario
}