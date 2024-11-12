using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float time = 0.3f;

    public GameObject enterMainMenu;
    public GameObject exitMainMenu;

    [SerializeField] TextMeshProUGUI gameText;

    public void Start()
    {
        enterMainMenu.SetActive(true);
        StartCoroutine(Delay(enterMainMenu));

        gameText.text = GameDataController.Instance.gameStarted() ? gameText.text = "Continue" : gameText.text = "New Game";

    }
    public IEnumerator Delay(GameObject gameObject)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
