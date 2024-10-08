using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerStats baseStats;
    public HealthBar healthBar;
    public Inventory inventory;
    [SerializeField] public LoseScreen loseScreen;

    IActorModel _player;
    PlayerView _playerView;
    public ClassManager classManager;

    public string nombreEscenaAJugar;

    public int actualHealth;


    private void Awake()
    {
        _player = GetComponent<IActorModel>();
        _playerView = GetComponent<PlayerView>();
        inventory = GetComponent<Inventory>();
        classManager = GetComponent<ClassManager>();
        string currentScene = SceneManager.GetActiveScene().name;

        Time.timeScale = 1.0f;

        UpdateClassStats();
    }

    private void Update()
    {
        Walk();
        UseInventoryItem();
        if(healthBar != null)
            HealthBarManager();
    }

    public void UpdateClassStats()
    {
        playerStats.maxHealth = baseStats.maxHealth;
        playerStats.damageMultiplier = baseStats.damageMultiplier;
        playerStats.movementSpeed = baseStats.movementSpeed;

        ClassManager.SelectedClass selectedClass = classManager.GetCurrentClass();

        foreach (ClassStats classStat in classManager.stats)
        {
            if (classStat.selectedClass == selectedClass)
            {
                playerStats.maxHealth += classStat.maxHealth;
                playerStats.damageMultiplier += classStat.damageMultiplier;
                playerStats.movementSpeed += classStat.movementSpeed;
            }
        }
        actualHealth = playerStats.maxHealth;
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
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            GameDataController.Instance.SaveData();
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
        healthBar.SetHealth(actualHealth);
        _playerView.anim.SetTrigger("Heal");
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
        baseStats = newStats;
        UpdateClassStats();
    }

    public void UpdateHealth(PlayerStats newStats)
    {
        baseStats = newStats;
        UpdateClassStats();
        actualHealth = playerStats.maxHealth;
    }

}
