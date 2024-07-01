using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    public Image XpBar;
    public Image[] XpPoints;

    public Slider slider;
    public TextMeshProUGUI textMeshPro;

    public int _Xp;
    public int maxXPText;

    public void SetMaxXP(int maxXP)
    {
        slider.maxValue = maxXP;
        slider.value = 0;
        maxXPText = maxXP;
        textMeshPro.text = "XP" + "0" + "/" + maxXP;
    }

    public void SetXP(int XP)
    {
        _Xp = XP;

        for (int i = 0; i < XpPoints.Length; i++)
        {
            XpPoints[i].enabled = !DisplayXpPoints(_Xp, i); 
        }

        textMeshPro.text = "XP" + XP + "/" + maxXPText;
    }

    bool DisplayXpPoints(int _Xp, int pointNumber)
    {
        return ((pointNumber * 6.25) >= _Xp);
    }
}
