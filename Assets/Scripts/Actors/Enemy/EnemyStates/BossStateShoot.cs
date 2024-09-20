using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateShoot<T> : State<T>
{
    private BossModel _model;
    private Transform _shootTarget;
    private BossView _view;

    public BossStateShoot(BossModel model, Transform shootTarget, BossView view)
    {
        _shootTarget = shootTarget;
        _model = model;
        _view = view;
    }

    public override void Execute()
    {
        base.Execute();
        _model.Move(Vector2.zero);
        _model.Shoot(_shootTarget.position);
    }

    public override void Sleep()
    {
        base.Sleep();
    }
}
