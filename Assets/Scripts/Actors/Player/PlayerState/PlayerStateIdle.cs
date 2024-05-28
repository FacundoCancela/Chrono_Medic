using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateIdle<T> : State<T>
{
    
    private IActorModel _player;
    private IActorView _playerView;
    T _InputMovement;
    //Este no necesita las variables de blackboard

    public PlayerStateIdle(IActorModel model, IActorView view, T InputMovement)
    {
        _player = model;
        _playerView = view;
        _InputMovement = InputMovement;
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Execute()
    {
        base.Execute();
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x != 0 || y != 0)
        {
            _fsm.Transition(_InputMovement);
        }
    }

    public override void Sleep()
    {

        base.Sleep();
        
    }
}

