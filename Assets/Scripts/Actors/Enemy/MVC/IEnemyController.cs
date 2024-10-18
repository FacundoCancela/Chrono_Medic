using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyController 
{
    public void GetDamaged(int damage);
    Transform transform { get; }
}
