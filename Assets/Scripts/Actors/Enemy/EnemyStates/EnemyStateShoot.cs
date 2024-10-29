using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateShoot<T> : State<T>
{
    private EnemyModel _model;
    private Transform _shootTarget;
    private EnemyView _view;
    //Este no necesita las variables de blackboard
    
    public EnemyStateShoot(EnemyModel model, Transform shootTarget, EnemyView view)
    {
        _shootTarget = shootTarget;
        _model = model;
        _view = view;
    }
    public override void Enter()
    {
        base.Enter();
        _model.Move(Vector2.zero);
    }
    public override void Execute()
    {
        base.Execute();
        if (_model.canUseWeapon)
        {
            _view.Attack(true);
            _model.Shoot(_shootTarget.position);
        }
        else
        {
            _view.Attack(false);
        }

    }
    public override void Sleep()
    {
        base.Sleep();
        _model.Move(new Vector2(_model.enemyStats.movementSpeed, _model.enemyStats.movementSpeed));
    }
}
