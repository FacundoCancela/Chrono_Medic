using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSwitchButton : MonoBehaviour
{
    public Canvas newCanvas;
    public Canvas previousCanvas;
    public GameObject enterSecondaryMenu;
    public GameObject newBackground;
    public GameObject previousBackground;

    public void Switch()
    {
        enterSecondaryMenu.SetActive(true);
        StartCoroutine(SwitchDelay(enterSecondaryMenu));
        // Activar el primer canvas y desactivar el segundo
        newCanvas.gameObject.SetActive(true);
        newBackground.gameObject.SetActive(true);
    }

    public IEnumerator SwitchDelay(GameObject gameObject)
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        previousCanvas.gameObject.SetActive(false);
        previousBackground.gameObject.SetActive(false);
    }
}
