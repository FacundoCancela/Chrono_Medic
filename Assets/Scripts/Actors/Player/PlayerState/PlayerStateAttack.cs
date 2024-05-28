using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStateAttack<T> : State<T>
{
    IActorView _view;
    T _InputMovement;
    T _InputIdle;
    IActorModel _Model;

    public PlayerStateAttack(IActorView view, IActorModel model, T inputMovement, T inputIdle)
    {
        _view = view;
        _InputMovement = inputMovement;
        _InputIdle = inputIdle;
        _Model = model;
    }

    public override void Enter()
    {
        base.Enter();
        _view.Attack(true);
    }

    public override void Execute()
    {
        base.Execute();
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y).normalized;
        _Model.Move(dir);
        _view.LookDir(dir);
        _fsm.Transition(_InputIdle);
    }

    public override void Sleep()
    {
        base.Sleep();
        _view.Attack(false);
        Debug.Log("SleepAttack");
    }
}
