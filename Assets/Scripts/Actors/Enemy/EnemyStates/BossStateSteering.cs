using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateSteering<T> : State<T>
{
    ISteering _steering;
    BossModel _enemy;
    BossView _enemyView;
    ObstacleAvoidance _obs;

    public BossStateSteering(BossModel enemy, BossView view, ISteering steering, ObstacleAvoidance obs)
    {
        _steering = steering;
        _enemy = enemy;
        _enemyView = view;
        _obs = obs;
    }

    public override void Execute()
    {
        var dir = _obs.GetDir(_steering.GetDir());
        _enemy.Move(dir);
        _enemyView.LookDir(dir);
    }
}
