using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateDead<T> : State<T>
{
    private BossModel _model;

    public BossStateDead(BossModel model)
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
