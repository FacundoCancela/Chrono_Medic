using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTree 
{
    private FSM<EnemyStatesEnum> _fsm;

    private Transform _target;
    private BossModel _model;
    private float _shootRange;

    private ITreeNode _root;

    private Dictionary<string, object> _blackboardDictionary;

    public BossTree(FSM<EnemyStatesEnum> fsm, BossModel model, Transform target, float shootRange, ref Dictionary<string, object> blackboardDictionary)
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
        ITreeNode idle = new ActionNode(() => _fsm.Transition(EnemyStatesEnum.idle));
        ITreeNode seek = new ActionNode(() => _fsm.Transition(EnemyStatesEnum.Seek));
        ITreeNode shoot = new ActionNode(() => _fsm.Transition(EnemyStatesEnum.Shoot));
        ITreeNode dead = new ActionNode(() => _fsm.Transition(EnemyStatesEnum.Dead));

        //Questions
        ITreeNode questionIsCooldown = new QuestionNode(Question_IsInCooldown, shoot, idle);
        ITreeNode questionShootingRange = new QuestionNode(Question_IsInShootingRange, questionIsCooldown, seek);
        ITreeNode questionIsDead = new QuestionNode(Question_IsDead, dead, questionShootingRange);

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
        return (bool)_blackboardDictionary[BossBlackBoardConsts.B__IS_DEAD];
    }

    private bool Question_IsInShootingRange()
    {
        return (Vector2.Distance(_target.position, _model.transform.position) <= _shootRange);
    }
    private bool Question_IsInCooldown()
    {
        if (_model.bossWeapon.canUseWeapon == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
