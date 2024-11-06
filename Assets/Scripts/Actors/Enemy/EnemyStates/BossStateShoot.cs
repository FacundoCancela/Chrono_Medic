using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateShoot<T> : State<T>
{
    private BossModel _model;
    private Transform _shootTarget;
    private Rigidbody2D _targetRb;
    private BossView _view;

    public BossStateShoot(BossModel model, Transform shootTarget, BossView view, Rigidbody2D targetRb)
    {
        _shootTarget = shootTarget;
        _model = model;
        _view = view;
        _targetRb = targetRb;
    }

    public override void Execute()
    {
        base.Execute();
        _view.Attack(true);
        _model.Move(Vector2.zero);
        _model.Shoot(_shootTarget.position, _targetRb);
    }

    public override void Sleep()
    {
        base.Sleep();
        _view.Attack(true);
    }
}
