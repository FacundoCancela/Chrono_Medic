using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSpawnPoint : MonoBehaviour
{
    [SerializeField] DamageInteraction Interaction;
    [SerializeField] Transform spawnPoint;
    [SerializeField] int interactionCooldown = 2;

    private int actualWave;
    public int respawnWave;

    private void Update()
    {
        actualWave = WaveManager.Instance.GetActualWave();

        if(actualWave == respawnWave)
        {
            ReactivateInteraction();
        }
    }

    public void ReactivateInteraction()
    {
        Interaction.gameObject.SetActive(true);
    }

    public void GetRespawnWave()
    {
        respawnWave = WaveManager.Instance.GetActualWave() + interactionCooldown;
    }

}
