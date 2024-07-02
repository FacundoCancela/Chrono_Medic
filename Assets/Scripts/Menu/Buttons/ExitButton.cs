#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void SalirDelJuego()
    {
#if UNITY_EDITOR
        // Detener la reproducci�n en el editor
        EditorApplication.isPlaying = false;
#else
        // Salir de la aplicaci�n
        if(gameManager.exitMainMenu != null)
            gameManager.exitMainMenu.SetActive(true);
        Application.Quit();
#endif
    }
}
