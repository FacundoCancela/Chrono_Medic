using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public PlayerStats playerStats;
    public HealthBar healthBar;
    [SerializeField] public LoseScreen loseScreen;

    FSM<PlayerStateEnum>  _fsm;
    IActorModel _player;
    IActorView _view;
    PlayerView _playerView;
    WeaponManager _weaponManager;
    public string nombreEscenaAJugar;

    public int actualHealth;


    private void Awake()
    {
        _player = GetComponent<IActorModel>();
        _view = GetComponent<IActorView>();
        actualHealth = playerStats.maxHealth;
        _weaponManager = GetComponent<WeaponManager>();
        _weaponManager.OnAttack = Attack;
        InitializedFSM();
    }

    private void Update()
    {
        
        if(healthBar != null)
            HealthBarManager();

        _fsm.OnUpdate();
    }

    void InitializedFSM()
    {
        _fsm = new FSM<PlayerStateEnum>();

        var walk = new PlayerStateWalk<PlayerStateEnum>(_player, _view, PlayerStateEnum.Idle);
        var idle = new PlayerStateIdle<PlayerStateEnum>(_player, _view, PlayerStateEnum.Walk);
        var attack = new PlayerStateAttack<PlayerStateEnum>(_view, _player, PlayerStateEnum.Walk, PlayerStateEnum.Idle);

        idle.AddTransition(PlayerStateEnum.Walk, walk);
        idle.AddTransition(PlayerStateEnum.Attack, attack);
        walk.AddTransition(PlayerStateEnum.Idle, idle);
        walk.AddTransition(PlayerStateEnum.Attack, attack);
        attack.AddTransition(PlayerStateEnum.Idle, idle);

        _fsm.SetInit(idle);
       
    }

    public void HealthBarManager()
    {
        if(actualHealth >= playerStats.maxHealth)
        {
            healthBar.SetMaxHealth(actualHealth);
        }
        else healthBar.SetHealth(actualHealth);    
    }

   

    public void GetHealed(int hpHealed)
    {
        actualHealth += hpHealed;
        if (actualHealth > playerStats.maxHealth) actualHealth = playerStats.maxHealth;
    }

    public void GetDamaged(int damage)
    {
        _view.GetDamaged();
        actualHealth -= damage;
        if (actualHealth <= 0)
            Die();
        
    }

    public void Die()
    {
        this.gameObject.SetActive(false);
        loseScreen.gameObject.SetActive(true);
    }

    public void UpdateStats(PlayerStats newStats)
    {
        playerStats = newStats;
        actualHealth = playerStats.maxHealth;
    }

    public void Attack()
    {
        _fsm.Transition(PlayerStateEnum.Attack);
    }

}
