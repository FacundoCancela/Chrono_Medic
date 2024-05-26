using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] LoseScreen loseScreen;

    private void Start()
    {
        Time.timeScale = 0.0f;
    }
}
