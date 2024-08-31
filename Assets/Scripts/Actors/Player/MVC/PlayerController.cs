using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public PlayerStats playerStats;
    public HealthBar healthBar;
    public Inventory inventory;
    [SerializeField] public LoseScreen loseScreen;

    IActorModel _player;
    PlayerView _playerView;

    public string nombreEscenaAJugar;

    public int actualHealth;


    private void Awake()
    {
        _player = GetComponent<IActorModel>();
        _playerView = GetComponent<PlayerView>();
        actualHealth = playerStats.maxHealth;
        inventory = GetComponent<Inventory>();
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        Walk();
        GetMoney();
        UseInventoryItem();
        if(healthBar != null)
            HealthBarManager();
    }

    public void GetMoney()
    {
        if(Input.GetKeyDown(KeyCode.F8))
        {
            GameDataController.Instance.IncreaseMoney(1000);
        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            GameDataController.Instance.DecreaseMoney(1000);
        }
    }


    public void Walk()
    {
        _playerView.Walk(true);
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y).normalized;
        _player.Move(dir);
        _playerView.LookDir(dir);
        if (x == 0 && y == 0)
        {
            _playerView.Walk(false);
        }
    }

    public void UseInventoryItem()
    {
        if(Input.GetKeyDown(KeyCode.E) && inventory != null)
        {
            inventory.useInjection();
            _playerView.anim.SetBool("Heal",true);
        }
        else
        {
            _playerView.anim.SetBool("Heal", false);
        }
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
        _playerView.GetDamaged();
        actualHealth -= damage;
        if (actualHealth <= 0)
            Die();
        
    }

    public void Die()
    {
        HealthBarManager();
        this.gameObject.SetActive(false);
        loseScreen.gameObject.SetActive(true);
    }

    public void UpdateStats(PlayerStats newStats)
    {
        playerStats = newStats;
    }

    public void UpdateHealth(PlayerStats newStats)
    {
        playerStats = newStats;
        actualHealth = playerStats.maxHealth;
    }
}
