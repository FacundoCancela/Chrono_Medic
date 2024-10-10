using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnList", menuName = "ScriptableObjects/SpawnList", order = 1)]

public class SpawnList : ScriptableObject
{
    [SerializeField] public List<GameObject> enemyPrefabs;
}
