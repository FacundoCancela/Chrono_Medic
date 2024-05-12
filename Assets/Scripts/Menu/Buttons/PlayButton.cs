using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    //public string nombreEscenaAJugar; // Nombre de la escena a la que quieres cambiar
    public void CambiarEscena(string nombreEscenaAJugar)
    {
        // Cambiar a la escena especificada
        SceneManager.LoadScene(nombreEscenaAJugar);
    }
}
