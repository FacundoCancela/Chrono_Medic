using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform jugador; // Referencia al transform del jugador

    public Vector2 offset; // Offset opcional para ajustar la posici�n de la c�mara

    void Update()
    {
        // Verificar si hay un jugador asignado
        if (jugador != null)
        {
            // Obtener la posici�n actual de la c�mara
            Vector3 posicionActual = transform.position;

            // Obtener la posici�n objetivo de la c�mara
            Vector3 posicionObjetivo = new Vector3(jugador.position.x + offset.x, jugador.position.y + offset.y, posicionActual.z);

            // Asignar la posici�n objetivo a la c�mara
            transform.position = posicionObjetivo;
        }
    }
}
