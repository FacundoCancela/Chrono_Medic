using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSwitchButton : MonoBehaviour
{
    public Canvas newCanvas;
    public Canvas previousCanvas;

    public void Switch()
    {
        // Activar el primer canvas y desactivar el segundo
        newCanvas.gameObject.SetActive(true);
        previousCanvas.gameObject.SetActive(false);
    }

}
