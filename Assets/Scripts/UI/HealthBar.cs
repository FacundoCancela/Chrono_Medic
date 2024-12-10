using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;               
    public Image[] healthPoints;          

    public TextMeshProUGUI textMeshPro;

    Animator anim;

    public int _health;                   
    public int maxHealthText;             
    private float _healthPercent;

    private void OnEnable()
    {
        PlayerController.OnHealthUpdated += UpdateHealthBar;
    }

    private void OnDisable()
    {
        PlayerController.OnHealthUpdated -= UpdateHealthBar;
    }

    private void UpdateHealthBar(int health, int maxHealth)
    {
        SetMaxHealth(maxHealth);
        SetHealth(health);
    }

    public void SetMaxHealth(int maxHealth)
    {
        maxHealthText = maxHealth;
        if(textMeshPro != null)
            textMeshPro.text = maxHealth.ToString();
        _healthPercent = maxHealth / (float)healthPoints.Length;
    }

    public void SetHealth(int health)
    {
        _health = health;

        // Actualiza el texto con la salud actual.
        if (textMeshPro != null)
            textMeshPro.text = _health.ToString() /*+ "/" + maxHealthText*/;

        // Actualiza el estado de cada punto de salud.
        for (int i = 0; i < healthPoints.Length; i++)
        {
            if (healthPoints[i] != null)
            {
                healthPoints[i].enabled = !DisplayHealthPoints(_health, i);
            }
        }
    }

    bool DisplayHealthPoints(int _health, int pointNumber)
    {
        return _health <= ((pointNumber) * _healthPercent);
    }
}
