using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DropManager;

public class Jarron : MonoBehaviour, IEnemyController
{
    public int actualHealth = 1;

    public void GetDamaged(int damage)
    {
        actualHealth -= damage;
        Break();
    }

    private void Break()
    {
        if (actualHealth <= 0)
        {
            RequestDrop(transform.position, DropType.Vase);
            Destroy(gameObject);
        }
    }


}
