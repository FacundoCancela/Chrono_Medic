using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateDead<T> : State<T>
{
    private EnemyModel _model;

    public EnemyStateDead(EnemyModel model)
    {
        _model = model;
    }

    public override void Execute()
    {
        base.Execute();
        GameObject.Destroy(_model.gameObject);
    }

}
