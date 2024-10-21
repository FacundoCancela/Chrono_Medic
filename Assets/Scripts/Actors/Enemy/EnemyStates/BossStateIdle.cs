using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateIdle<T> : State<T>
{
    private BossModel _model;
    private BossView _view;

    public BossStateIdle(BossModel model, BossView view)
    {
        _model = model;
        _view = view;
    }
}
