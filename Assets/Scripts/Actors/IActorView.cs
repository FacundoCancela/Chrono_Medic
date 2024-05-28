using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActorView 
{
    void LookDir(Vector2 dir);
    void GetDamaged();
    void Walk(bool IsWalking);
    void Attack(bool IsAttack);
}