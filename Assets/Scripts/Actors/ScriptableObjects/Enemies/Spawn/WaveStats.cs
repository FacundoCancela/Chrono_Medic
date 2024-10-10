using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveStats", menuName = "ScriptableObjects/WaveStats", order = 1)]

public class WaveStats : ScriptableObject
{
    public List<int> maxEnemiesInThisWave;
    public List<float> spawnInteval;
    public List<float> waveTimer;

}
