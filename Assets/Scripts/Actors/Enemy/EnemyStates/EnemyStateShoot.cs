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

    public override void Execute()
    {
        base.Execute();

        _view.Attack(true);
        //Anim
        //_model.animator.SetTrigger("Attack");
        //Dispara con autoaim al jugador
        _model.Move(Vector2.zero);
        _model.Shoot(_shootTarget.position);
    }
}
