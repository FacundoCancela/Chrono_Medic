using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveCount : MonoBehaviour
{
    public TextMeshProUGUI text;

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


}
