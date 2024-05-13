using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField] public string nombreEscenaAJugar;

    public void CambiarEscenaDefault()
    {
        // Cambiar a la escena especificada
        UnityEngine.SceneManagement.SceneManager.LoadScene(nombreEscenaAJugar);
    }

    public void CambiarEscena(string nombreEscenaAJugar)
    {
        // Cambiar a la escena especificada
        UnityEngine.SceneManagement.SceneManager.LoadScene(nombreEscenaAJugar);
    }
}
