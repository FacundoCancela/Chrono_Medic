using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI textMeshPro;

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
        slider.value = XP;
        textMeshPro.text = "XP" + XP + "/" + maxXPText;
    }
}
