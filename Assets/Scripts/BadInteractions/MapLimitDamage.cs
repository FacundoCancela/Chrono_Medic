using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapLimitDamage : MonoBehaviour
{
    [SerializeField] private int stormDamage;
    [SerializeField] private bool playerIsOutsideMap;
    [SerializeField] private float playerOutsideLimitTime = 10f;
    [SerializeField] private float damageTimer = 0f;
    [SerializeField] private float timeLeft;
    [SerializeField] private GameObject outsideCanvas;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private PlayerController playerController;


    void Start()
    {
        timeLeft = playerOutsideLimitTime;
    }


    void Update()
    {
        if(playerIsOutsideMap)
        {
            timeLeft -= Time.deltaTime;
            timeLeft = Mathf.Clamp(timeLeft, 0f, playerOutsideLimitTime);
            timerText.text = Mathf.Ceil(timeLeft).ToString();

            if (timeLeft <= 0f)
            {
                damageTimer += Time.deltaTime;
                if (damageTimer >= 1f)
                {
                    playerController.GetDamaged(stormDamage);
                    damageTimer = 0f;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerIsOutsideMap = true;
            outsideCanvas.SetActive(true);
            timeLeft = playerOutsideLimitTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsOutsideMap = false;
            outsideCanvas.SetActive(false);
            timeLeft = playerOutsideLimitTime;
            damageTimer = 0f;
        }
    }


}
