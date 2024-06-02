using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalWeapon : MonoBehaviour
{
    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public Transform playerController;
    [SerializeField] public Transform weapon;

    public void Update()
    {
        weapon.RotateAround(playerController.position, new Vector3(0,0,1), playerStats.orbitalSpeed * Time.deltaTime);
    }


}
