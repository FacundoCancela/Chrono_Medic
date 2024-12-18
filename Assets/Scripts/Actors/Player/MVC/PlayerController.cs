using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static event Action<int, int> OnHealthUpdated;
    public static event Action OnPlayerDeath;

    private PauseManager pauseManager;
    private DialogueManager dialogueManager;

    public PlayerStats playerStats;
    public PlayerStats baseStats;
    public Inventory inventory;

    public Animator animHealth;

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
        TriggerHealthUpdated();
    }

    private void Update()
    {
        if (pauseManager != null && pauseManager.gamePaused)
        {
            return; // Salir del Update si el juego est� pausado
        }

        if (dialogueManager != null && dialogueManager.DialogeActive)
        {
            return; // Salir del Update si el juego est� pausado
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
    }

    public void UpdateCursorState()
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
   
    public void GetHealed(int hpHealed)
    {
        actualHealth += hpHealed;
        if (actualHealth > playerStats.maxHealth) actualHealth = playerStats.maxHealth;
        _playerView.anim.SetTrigger("Heal");
        TriggerHealthUpdated();
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
        TriggerHealthUpdated();
    }

    private void TriggerHealthUpdated()
    {
        OnHealthUpdated?.Invoke(actualHealth, playerStats.maxHealth);
    }

    public void Die()
    {
        playerAlive = false;
        playerControllable = false;
        UpdateCursorState();
        _player.Move(Vector2.zero);
        TriggerHealthUpdated();
        StartCoroutine(DeathCoroutine());
    }
    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
        OnPlayerDeath?.Invoke();
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
    public void SetAnim()
    {
        if (actualHealth >= (playerStats.maxHealth * 7) / 10)
        {
            animHealth.SetBool("Azul", true);
            animHealth.SetBool("Amarillo", false);
            animHealth.SetBool("Rojo", false);
        }
        else if (actualHealth >= (playerStats.maxHealth * 5) / 10)
        {
            animHealth.SetBool("Azul", false);
            animHealth.SetBool("Amarillo", true);
            animHealth.SetBool("Rojo", false);
        }
        else 
        {
            animHealth.SetBool("Azul", false);
            animHealth.SetBool("Amarillo", false);
            animHealth.SetBool("Rojo", true);
        }
    }

}
