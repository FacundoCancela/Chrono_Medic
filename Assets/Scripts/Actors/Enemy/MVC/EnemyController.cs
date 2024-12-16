using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;


public static class EnemyBlackBoardConsts
{
    public static string B__IS_DEAD = "bool_IsDead";
}

public class EnemyController : MonoBehaviour, IEnemyController
{
    public EnemyStats enemyStats;

   
    EnemyModel _model;
    EnemyView _view;
    [SerializeField] PlayerController _target;
    [SerializeField] Rigidbody2D _targetRb;
    EnemyTree _enemyTree;

    ObstacleAvoidance _obstacleAvoidance;
    public float angle;
    public float radius;
    public LayerMask obsMask;

    FSM<EnemyStatesEnum> _fsm;
    ISteering _steering;

    public float personalSpaceRadius = 2.0f; 
    public LayerMask enemyLayer; 
    public float separationStrength = 1.0f; 

   
    private float shootRange;
    public int actualHealth;

    private Dictionary<string, object> _blackBoardDictionary = new Dictionary<string, object>
    {
        {EnemyBlackBoardConsts.B__IS_DEAD,false },
    };

    private void Awake()
    {
        
        _model = GetComponent<EnemyModel>();
        _view = GetComponent<EnemyView>();
        _target = FindObjectOfType<PlayerController>();

        if (_target != null)
        {
            _targetRb = _target.GetComponent<Rigidbody2D>();
        }

       
        InitilizeSteering();
        InitializeFSM();

        //EnemyStats
        shootRange = enemyStats.attackRange;
        actualHealth = enemyStats.maxHealth;

        _enemyTree = new EnemyTree(_fsm, _model, _target.transform, shootRange, ref _blackBoardDictionary);
        _enemyTree.InitializeTree();

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
        var idle = new EnemyStateIdle<EnemyStatesEnum>(_model, _view);
        var seek = new EnemyStateSteering<EnemyStatesEnum>(_model, _view, _steering, _obstacleAvoidance);
        var shoot = new EnemyStateShoot<EnemyStatesEnum>(_model, _target.transform, _view, _targetRb);
        var dead = new EnemyStateDead<EnemyStatesEnum>(_model);

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
        _enemyTree.ExecuteTree();
        if (!(_fsm.CurrentState is EnemyStateShoot<EnemyStatesEnum>))
        {
            ApplyFlocking();
        }
    }

    private void ApplyFlocking()
    {
        Vector2 separationForce = Separation();

        if (separationForce != Vector2.zero)
        {
            _model.transform.position += (Vector3)separationForce * Time.deltaTime;
        }
    }

    private Vector2 Separation()
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, personalSpaceRadius, enemyLayer);
        Vector2 separationForce = Vector2.zero;

        foreach (Collider2D enemy in nearbyEnemies)
        {
            if (enemy.transform != transform)
            {
                Vector2 directionAway = (Vector2)(transform.position - enemy.transform.position);
                float distance = directionAway.magnitude;
                separationForce += directionAway.normalized / distance;
            }
        }

        return separationForce * separationStrength;
    }

    private void DeathCheck()
    {
        if (actualHealth <= 0)
        {
            _view.anim.SetTrigger("Dead");
            _blackBoardDictionary[EnemyBlackBoardConsts.B__IS_DEAD] = true;
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
