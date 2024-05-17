using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        _model.EnemyDeath();
        GameObject.Destroy(_model.gameObject);
    }

}
