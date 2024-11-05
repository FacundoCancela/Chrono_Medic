using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    public enum DropType
    {
        Enemy,
        Boss,
        Vase
    }

    public List<GameObject> dropPrefabs;

    public List<int> enemyDropWeights;
    public List<int> bossDropWeights;
    public List<int> vaseDropWeights;


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

    public void DropSomething(Vector3 dropPosition, DropType dropType)
    {
        List<int> selectedDropWeights;

        switch (dropType)
        {
            case DropType.Enemy:
                selectedDropWeights = enemyDropWeights;
                break;
            case DropType.Boss:
                selectedDropWeights = bossDropWeights;
                break;
            case DropType.Vase:
                selectedDropWeights = vaseDropWeights;
                break;
            default:
                return;
        }

        if (dropPrefabs.Count != selectedDropWeights.Count)
        {
            return;
        }

        int totalWeight = 0;
        for (int i = 0; i < selectedDropWeights.Count; i++)
        {
            totalWeight += selectedDropWeights[i];
        }

        int randomWeight = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;
        for (int i = 0; i < dropPrefabs.Count; i++)
        {
            cumulativeWeight += selectedDropWeights[i];
            if (randomWeight < cumulativeWeight)
            {
                Instantiate(dropPrefabs[i], dropPosition, Quaternion.identity);
                return;
            }
        }
    }

}
