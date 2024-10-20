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
        var dir = _obs.GetDir(_steering.GetDir());
        _enemy.Move(dir);
        _enemyView.LookDir(dir);
        _enemyView.Walk(true);
    }
    public override void Sleep()
    {
        base.Sleep();
        _enemyView.Walk(false);
    }
}
