using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPivot : MonoBehaviour
{
    [SerializeField] public Transform player;

    
    void Update()
    {
        transform.position = player.position;
    }
}
