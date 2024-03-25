using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyView
{
    void LookDir(Vector2 dir);
    void GetDamaged();
}
