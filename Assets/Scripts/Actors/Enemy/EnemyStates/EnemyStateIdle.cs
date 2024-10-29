using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateIdle<T> : State<T>
{
    private EnemyModel _model;
    private EnemyView _view;

    public EnemyStateIdle(EnemyModel model, EnemyView view)
    {
        _model = model;
        _view = view;
    }
    public override void Execute()
    {
        base.Execute();
        _model.Move(Vector2.zero);
    }
    public override void Sleep()
    {
        base.Sleep();
        _model.Move(new Vector2(_model.enemyStats.movementSpeed, _model.enemyStats.movementSpeed));
    }

}
