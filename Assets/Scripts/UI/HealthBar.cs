using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;               
    public Image[] healthPoints;          

    public TextMeshProUGUI textMeshPro;   

    public int _health;                   
    public int maxHealthText;             
    private float _healthPercent;         

    public void SetMaxHealth(int maxHealth)
    {
        maxHealthText = maxHealth;
        textMeshPro.text = "HP " + maxHealth + "/" + maxHealth;
        _healthPercent = maxHealth / (float)healthPoints.Length;
    }

    public void SetHealth(int health)
    {
        _health = health;

        // Actualiza el texto con la salud actual.
        textMeshPro.text = "HP " + _health + "/" + maxHealthText;

        // Actualiza el estado de cada punto de salud.
        for (int i = 0; i < healthPoints.Length; i++)
        {
            healthPoints[i].enabled = !DisplayHealthPoints(_health, i);
        }
    }

    bool DisplayHealthPoints(int _health, int pointNumber)
    {
        return _health <= ((pointNumber) * _healthPercent);
    }
}
