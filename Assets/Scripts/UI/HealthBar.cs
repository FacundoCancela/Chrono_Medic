using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public Image[] healthPoints;

    public Slider slider;
    public TextMeshProUGUI textMeshPro;


    public int _health;
    public int maxHealthText;
    public int _healthPercent;

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        maxHealthText = maxHealth;
        textMeshPro.text = "HP" + maxHealth + "/" + maxHealth;
        _healthPercent = maxHealthText / 100;
    }

    public void SetHealth(int health)
    {
        _health = health;

        for (int i = 0; i < healthPoints.Length; i++)
        {
            healthPoints[i].enabled = !DisplayHealthPoints(_health,i);
        }

        textMeshPro.text = "HP" + _health + "/" + maxHealthText;
    }

    bool DisplayHealthPoints(int _health,int pointNumber)
    {
        return (((pointNumber + 1) * 10) >= _health/_healthPercent );
    }
}