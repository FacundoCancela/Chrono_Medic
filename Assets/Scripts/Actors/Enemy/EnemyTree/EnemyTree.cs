using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTree
{
    private FSM<EnemyStatesEnum> _fsm;

    private Transform _target;
    private Transform _model;
    private float _shootRange;

    private ITreeNode _root;

    private Dictionary<string, object> _blackboardDictionary;

    public EnemyTree(FSM<EnemyStatesEnum> fsm,Transform model ,Transform target, float shootRange,ref Dictionary<string, object> blackboardDictionary)
    {
        _fsm = fsm;
        _target = target;
        _shootRange = shootRange;
        _model = model;

        _blackboardDictionary = blackboardDictionary;
    }

    public void InitializeTree()
    {
        //Actions
        ITreeNode seek = new ActionNode(() => _fsm.Transition(EnemyStatesEnum.Seek));
        ITreeNode shoot = new ActionNode(() => _fsm.Transition(EnemyStatesEnum.Shoot));
        ITreeNode dead = new ActionNode(() => _fsm.Transition(EnemyStatesEnum.Dead));

        //Questions
        ITreeNode questionShootingRange = new QuestionNode(Question_IsInShootingRange, shoot, seek);
        ITreeNode questionIsDead = new QuestionNode(Question_IsDead, dead,questionShootingRange);

        _root = questionIsDead;
    }

    public void changeTree(ITreeNode newTree)
    {
        _root = newTree;
    }

    public void ExecuteTree()
    {
        _root.Execute();
    }

    private bool Question_IsDead()
    {
        return (bool)_blackboardDictionary[EnemyBlackBoardConsts.B__IS_DEAD];
    }

    private bool Question_IsInShootingRange()
    {
        return (Vector2.Distance(_target.position,_model.position) <= _shootRange );
    }
}
