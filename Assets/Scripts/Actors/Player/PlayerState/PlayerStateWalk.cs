using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateWalk<T> : State<T>
{
    private IActorModel _player;
    T _Idle;
    private IActorView _playerView;

    //Este no necesita las variables de blackboard

    public PlayerStateWalk(IActorModel model, IActorView view, T Idle)
    {
        _player = model;
        _playerView = view;
        _Idle = Idle;
    }

    public override void Execute()
    {
        base.Execute();

        _playerView.Walk(true);

        _player.Move(Vector2.zero);

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y).normalized;
        _player.Move(dir);
        _playerView.LookDir(dir);
        if (x == 0 && y == 0)
        {
            _fsm.Transition(_Idle);
        }

    }

    public override void Sleep()
    {

        base.Sleep();
        _playerView.Walk(false);
    }
}


