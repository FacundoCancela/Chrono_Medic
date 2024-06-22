using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    public enum DropType
    {
        experience,
        injection
    }

    public List<GameObject> dropPrefabs;

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
        int randomIndex = Random.Range(0, dropPrefabs.Count);
        GameObject dropObject = dropPrefabs[randomIndex];
        Instantiate(dropObject,dropPosition, Quaternion.identity);
    }

}
