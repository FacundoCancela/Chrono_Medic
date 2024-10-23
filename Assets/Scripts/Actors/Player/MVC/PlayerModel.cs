using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : Actor
{
    [SerializeField] PlayerStats stats;
    public override void Move(Vector2 dir)
    {
        dir *= stats.movementSpeed;
        base.Move(dir);
    }


}
