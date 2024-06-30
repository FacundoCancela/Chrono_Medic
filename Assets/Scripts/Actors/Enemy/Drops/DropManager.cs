using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    public List<GameObject> dropPrefabs;
    public List<int> dropWeights;

    public static DropManager Instance
    {
        get { return instance; }
    }
    private static DropManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DropSomething(Vector3 dropPosition)
    {
        if (dropPrefabs.Count != dropWeights.Count)
        {
            return;
        }

        // Calculate the total weight
        int totalWeight = 0;
        for (int i = 0; i < dropWeights.Count; i++)
        {
            totalWeight += dropWeights[i];
        }

        // Generate a random number between 0 and the total weight
        int randomWeight = Random.Range(0, totalWeight);

        // Determine which item to drop based on the random weight
        int cumulativeWeight = 0;
        for (int i = 0; i < dropPrefabs.Count; i++)
        {
            cumulativeWeight += dropWeights[i];
            if (randomWeight < cumulativeWeight)
            {
                Instantiate(dropPrefabs[i], dropPosition, Quaternion.identity);
                return;
            }
        }
    }

}
