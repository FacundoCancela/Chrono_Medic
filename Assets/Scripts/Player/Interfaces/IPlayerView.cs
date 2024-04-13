using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerView 
{
    void LookDir(Vector2 dir);
    void GetDamaged();
}
