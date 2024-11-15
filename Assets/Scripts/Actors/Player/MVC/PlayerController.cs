using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private PauseManager pauseManager;
    private DialogueManager dialogueManager;

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

    public bool playerAlive = true;
    public bool playerControllable = true;


    private void Awake()
    {
        pauseManager = FindObjectOfType<PauseManager>();
        dialogueManager = FindObjectOfType<DialogueManager>();


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
        if (pauseManager != null && pauseManager.gamePaused)
        {
            return; // Salir del Update si el juego está pausado
        }

        if (dialogueManager != null && dialogueManager.DialogeActive)
        {
            return; // Salir del Update si el juego está pausado
        }

        if (playerAlive)
        {
            UpdateCursorState();

            if (playerControllable)
            {
                Walk();
                UseInventoryItem();
            }
        }

        if (healthBar != null)
            HealthBarManager();

        
    }

    private void UpdateCursorState()
    {
        if (playerControllable)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void UpdateClassStats()
    {
        playerStats.maxHealth = baseStats.maxHealth;
        playerStats.damageMultiplier = baseStats.damageMultiplier;
        playerStats.movementSpeed = baseStats.movementSpeed;
        playerStats.defensePercentage = baseStats.defensePercentage;

        ClassManager.SelectedClass selectedClass = classManager.GetCurrentClass();

        foreach (ClassStats classStat in classManager.stats)
        {
            if (classStat.selectedClass == selectedClass)
            {
                playerStats.maxHealth += classStat.maxHealth;
                playerStats.damageMultiplier += classStat.damageMultiplier;
                playerStats.movementSpeed += classStat.movementSpeed;
                playerStats.defensePercentage += classStat.defensePercentage;
                playerStats.ultimateCooldown = classStat.ultimateCooldown;
                playerStats.ultimateDuration = classStat.ultimateDuration;
            }
        }
        actualHealth = playerStats.maxHealth;
    }




    public void Walk()
    {
        _playerView.Walk(true);

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

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
        if (Input.GetKeyDown(KeyCode.F9))
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
        int reducedDamage = damage - (damage * playerStats.defensePercentage / 100);
        actualHealth -= reducedDamage;
        if (actualHealth <= 0)
        {
            _playerView.anim.SetTrigger("Dead");
            Die();
        }
    }

    public void Die()
    {
        playerAlive = false;
        playerControllable = false;
        UpdateCursorState();
        _player.Move(Vector2.zero);
        HealthBarManager();
        StartCoroutine(DeathCoroutine());
    }
    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1f);
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
