using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public PlayerStats playerStats;
    public HealthBar healthBar;
    [SerializeField] public LoseScreen loseScreen;

    IActorModel _player;
    IActorView _view;

    public string nombreEscenaAJugar;

    public int actualHealth;


    private void Awake()
    {
        _player = GetComponent<IActorModel>();
        _view = GetComponent<IActorView>();
        actualHealth = playerStats.maxHealth;
    }

    private void Update()
    {
        Move();
        if(healthBar != null)
            HealthBarManager();
    }

    public void HealthBarManager()
    {
        if(actualHealth >= playerStats.maxHealth)
        {
            healthBar.SetMaxHealth(actualHealth);
        }
        else healthBar.SetHealth(actualHealth);    
    }

    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y).normalized;
        _player.Move(dir);
        _view.LookDir(dir);
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


}
