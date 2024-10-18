using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    public Image XpBar;                   
    public Image[] XpPoints;              

    public Slider slider;                 
    public TextMeshProUGUI textMeshPro;   

    private int _Xp;                      
    private int maxXPText;                
    private float _xpPercent;             

    public void SetMaxXP(int maxXP)
    {
        slider.maxValue = maxXP;
        slider.value = 0; // Comienza desde 0 experiencia.
        maxXPText = maxXP;
        if(textMeshPro != null)
            textMeshPro.text = "XP " + "0" + "/" + maxXP;

        // Calcula el porcentaje de experiencia por punto.
        _xpPercent = maxXP / (float)XpPoints.Length;
    }

    public void SetXP(int XP)
    {
        _Xp = XP;

        // Actualiza el estado de cada punto de experiencia.
        for (int i = 0; i < XpPoints.Length; i++)
        {
            XpPoints[i].enabled = !DisplayXpPoints(_Xp, i);
        }

        // Actualiza el texto con la experiencia actual.
        if (textMeshPro != null)
            textMeshPro.text = "XP " + XP + "/" + maxXPText;

        // Actualiza el valor de la barra.
        slider.value = XP;
    }

    bool DisplayXpPoints(int _Xp, int pointNumber)
    {
        return _Xp <= ((pointNumber + 1) * _xpPercent);
    }
}

