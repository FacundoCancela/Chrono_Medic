using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public PlayerStats playerStats;

    IActorModel _player;
    IActorView _view;

    public string nombreEscenaAJugar;

    private int actualHealth;

    private void Awake()
    {
        _player = GetComponent<IActorModel>();
        _view = GetComponent<IActorView>();
        actualHealth = playerStats.maxHealth;
    }

    private void Update()
    {
        Move();

        if (actualHealth <= 0)
        {
            Die();
        }
    }

    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y).normalized;
        _player.Move(dir);
        _view.LookDir(dir);
    }

    public void GetDamaged(int damage)
    {
        _view.GetDamaged();
        actualHealth -= damage;
    }

    public void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nombreEscenaAJugar);
    }
}
