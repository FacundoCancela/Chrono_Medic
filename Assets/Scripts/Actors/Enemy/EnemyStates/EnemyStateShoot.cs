using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateShoot<T> : State<T>
{
    private EnemyModel _model;
    private Transform _shootTarget;
    
    //Este no necesita las variables de blackboard
    
    public EnemyStateShoot(EnemyModel model, Transform shootTarget)
    {
        _shootTarget = shootTarget;
        _model = model;
    }

    public override void Execute()
    {
        base.Execute();
        //Dispara con autoaim al jugador
        _model.Shoot(_shootTarget.position);
    }
}
