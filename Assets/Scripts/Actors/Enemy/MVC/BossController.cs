using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public static class BossBlackBoardConsts
{
    public static string B__IS_DEAD = "bool_IsDead";
}

public class BossController : MonoBehaviour, IEnemyController
{
    public EnemyStats enemyStats;

    //Variables de IA
    BossModel _model;
    BossView _view;
    [SerializeField] PlayerController _target;
    [SerializeField] Rigidbody2D _targetRb;
    BossTree _bossTree;

    ObstacleAvoidance _obstacleAvoidance;
    public float angle;
    public float radius;
    public LayerMask obsMask;

    FSM<EnemyStatesEnum> _fsm;
    ISteering _steering;

    //Variables
    private float shootRange;
    public int actualHealth;

    private Dictionary<string, object> _blackBoardDictionary = new Dictionary<string, object>
    {
        {BossBlackBoardConsts.B__IS_DEAD,false },
    };

    private void Awake()
    {
        //referencias
        _model = GetComponent<BossModel>();
        _view = GetComponent<BossView>();
        _target = FindObjectOfType<PlayerController>();

        if (_target != null)
        {
            _targetRb = _target.GetComponent<Rigidbody2D>();
        }


        //inicializaciones 
        InitilizeSteering();
        InitializeFSM();

        //EnemyStats
        shootRange = enemyStats.attackRange;
        actualHealth = enemyStats.maxHealth;

        _bossTree = new BossTree(_fsm, _model, _target.transform, shootRange, ref _blackBoardDictionary);
        _bossTree.InitializeTree();

    }

    void InitilizeSteering()
    {
        var seek = new Seek(_model.transform, _target.transform);

        _steering = seek;
        _obstacleAvoidance = new ObstacleAvoidance(_model.transform, angle, radius, obsMask);
    }

    void InitializeFSM()
    {
        _fsm = new FSM<EnemyStatesEnum>();

        //States
        var idle = new BossStateIdle<EnemyStatesEnum>(_model, _view);
        var seek = new BossStateSteering<EnemyStatesEnum>(_model, _view, _steering, _obstacleAvoidance);
        var shoot = new BossStateShoot<EnemyStatesEnum>(_model, _target.transform, _view, _targetRb);
        var dead = new BossStateDead<EnemyStatesEnum>(_model);

        //Transitions

        idle.AddTransition(EnemyStatesEnum.idle, seek);
        idle.AddTransition(EnemyStatesEnum.idle, shoot);

        seek.AddTransition(EnemyStatesEnum.Shoot, shoot);
        seek.AddTransition(EnemyStatesEnum.Dead, dead);

        shoot.AddTransition(EnemyStatesEnum.Seek, seek);
        shoot.AddTransition(EnemyStatesEnum.Dead, dead);

        dead.AddTransition(EnemyStatesEnum.Shoot, shoot);
        dead.AddTransition(EnemyStatesEnum.Seek, seek);

        _fsm.SetInit(seek);
    }


    private void Update()
    {
        _fsm.OnUpdate();
        _bossTree.ExecuteTree();
    }

    private void DeathCheck()
    {
        if (actualHealth <= 0)
        {
            _view.anim.SetTrigger("Dead");
            _blackBoardDictionary[BossBlackBoardConsts.B__IS_DEAD] = true;
        }
    }

    public void GetDamaged(int damage)
    {
        _view.GetDamaged();
        actualHealth -= damage;
        DeathCheck();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
}
