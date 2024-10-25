using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateSteering<T> : State<T>
{
    ISteering _steering;
    EnemyModel _enemy;
    EnemyView _enemyView;
    ObstacleAvoidance _obs;

    public EnemyStateSteering(EnemyModel enemy ,EnemyView view,ISteering steering, ObstacleAvoidance obs)
    {
        _steering = steering;
        _enemy = enemy;
        _enemyView = view;
        _obs = obs;
    }

    public override void Execute()
    {
        _enemyView.Walk(true);
        var dir = _obs.GetDir(_steering.GetDir());
        _enemy.Move(dir);
        _enemyView.LookDir(dir);
    }
    public override void Sleep()
    {
        base.Sleep();
        _enemyView.Walk(false);
    }
}
