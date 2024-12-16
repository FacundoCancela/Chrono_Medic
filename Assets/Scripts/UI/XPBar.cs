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
        slider.value = 0; 
        maxXPText = maxXP;
        if(textMeshPro != null)
            textMeshPro.text = "XP " + "0" + "/" + maxXP;

       
        _xpPercent = maxXP / (float)XpPoints.Length;
    }

    public void SetXP(int XP)
    {
        _Xp = XP;

   
        for (int i = 0; i < XpPoints.Length; i++)
        {
            if (XpPoints[i] != null)
            {
                XpPoints[i].enabled = !DisplayXpPoints(_Xp, i);
            }
        }

        
        if (textMeshPro != null)
            textMeshPro.text = "XP " + XP + "/" + maxXPText;

      
        slider.value = XP;
    }

    bool DisplayXpPoints(int _Xp, int pointNumber)
    {
        return _Xp <= ((pointNumber + 1) * _xpPercent);
    }
}

