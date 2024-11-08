using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocodileWarning : MonoBehaviour
{
    public GameObject crocodile;
    public float warningTime;

    void Start()
    {
        StartCoroutine(InstantiateAndDestroy());
    }

    private IEnumerator InstantiateAndDestroy()
    {
        yield return new WaitForSeconds(warningTime);
        Instantiate(crocodile, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
