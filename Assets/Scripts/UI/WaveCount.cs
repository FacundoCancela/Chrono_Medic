using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class WaveCount : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI timerText;

    public void updateWave(int actualWave, int maxWave)
    {
        if (actualWave < maxWave)
        {
            text.text = ("Waves:" + actualWave + "/" + maxWave);
        }
        else
        {
            text.text = ("Waves:" + maxWave + "/" + maxWave);
        }
    }

    public void UpdateTimer(float timer)
    {
        int intTimer = Mathf.RoundToInt(Mathf.Max(0, timer));
        timerText.text = ("Time left: " + intTimer.ToString());
    }

}
