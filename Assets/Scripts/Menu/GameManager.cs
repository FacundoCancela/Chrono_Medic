using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enterMainMenu;
    public GameObject exitMainMenu;
    public void Start()
    {
        enterMainMenu.SetActive(true);
        StartCoroutine(Delay(enterMainMenu));
    }
    public IEnumerator Delay(GameObject gameObject)
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
