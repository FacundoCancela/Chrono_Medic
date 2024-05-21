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

    private int actualHealth;


    private void Awake()
    {
        _player = GetComponent<IActorModel>();
        _view = GetComponent<IActorView>();
        actualHealth = playerStats.maxHealth;
        if(healthBar != null )
        {
            healthBar.SetMaxHealth(actualHealth);
        }
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y).normalized;
        _player.Move(dir);
        _view.LookDir(dir);
    }

    private void DeathCheck()
    {
        if (actualHealth <= 0)
        {
            Die();
        }
    }

    public void GetDamaged(int damage)
    {
        _view.GetDamaged();
        actualHealth -= damage;
        DeathCheck();
        healthBar.SetHealth(actualHealth);
    }

    public void Die()
    {
        this.gameObject.SetActive(false);
        loseScreen.gameObject.SetActive(true);
    }
}
