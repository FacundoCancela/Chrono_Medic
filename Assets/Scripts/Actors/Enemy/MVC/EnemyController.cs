using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Variables de IA
    EnemyView _view;
    PlayerController _target;
    public Rigidbody2D _targetRb;
    public float timePrediction;
    public float angle;
    public float radius;
    public LayerMask obsMask;
    FSM<StatesEnum> _fsm;
    ISteering _steering;
    EnemyModel _enemy;
    ObstacleAvoidance _obstacleAvoidance;

    //Variables de ataque
    bool canAttack = true; 
    public float attackRange = 2f; 
    public int damage = 2;
    public float attackCooldown = 1f; 

    //Variables de vida
    public int maxHealth = 10;
    public int actualHealth;



    private void Awake()
    {
        //referencias
        _enemy = GetComponent<EnemyModel>();
        _view = GetComponent<EnemyView>();
        _target = FindObjectOfType<PlayerController>();
        _targetRb = FindObjectOfType<PlayerController>().GetComponent<Rigidbody2D>();
        //inicializaciones 
        InitilizeSteering();
        InitializeFSM();
    }

    private void Start()
    {
        actualHealth = maxHealth;
    }

    void InitilizeSteering()
    {
        var seek = new Seek(_enemy.transform, _target.transform);
        var flee = new Flee(_enemy.transform, _target.transform);
        var pursuit = new Pursuit(_enemy.transform, _targetRb, timePrediction);
        var evade = new Evade(_enemy.transform, _targetRb, timePrediction);
        _steering = seek;
        _obstacleAvoidance = new ObstacleAvoidance(_enemy.transform, angle, radius, obsMask);
    }

    void InitializeFSM()
    {
        _fsm = new FSM<StatesEnum>();

        var idle = new EnemyStateIdle<StatesEnum>();
        var steering = new EnemyStateSteering<StatesEnum>(_enemy,_view, _steering, _obstacleAvoidance);

        idle.AddTransition(StatesEnum.Walk, steering);
        steering.AddTransition(StatesEnum.Idle, idle);

        _fsm.SetInit(steering);
    }


    private void Update()
    {
        if (actualHealth <= 0)
        {
            Die();
        }

        _fsm.OnUpdate();

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && canAttack)
        {
            Attack();
            StartCoroutine(AttackCooldown()); // Iniciar el cooldown del ataque
        }
    }

    public void GetDamaged(int damage)
    {
        _view.GetDamaged();
        actualHealth -= damage;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Attack()
    {
        _target.GetDamaged(damage);
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false; // Desactivar la capacidad de atacar
        yield return new WaitForSeconds(attackCooldown); // Esperar el tiempo de cooldown
        canAttack = true; // Activar la capacidad de atacar nuevamente
    }
}
