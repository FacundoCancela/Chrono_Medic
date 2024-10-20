using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateIdle<T> : State<T>
{
    private EnemyModel _model;
    private EnemyView _view;

    public EnemyStateIdle(EnemyModel model, EnemyView view)
    {
        _model = model;
        _view = view;
    }

}
