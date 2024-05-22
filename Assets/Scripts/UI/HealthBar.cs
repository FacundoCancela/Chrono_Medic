using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI textMeshPro;

    public int maxHealthText;

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        maxHealthText = maxHealth;
        textMeshPro.text = "HP" + maxHealth + "/" + maxHealth;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        textMeshPro.text = "HP" + health + "/" + maxHealthText;
    }
}