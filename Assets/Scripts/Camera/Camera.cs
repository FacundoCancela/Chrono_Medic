using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform jugador; // Referencia al transform del jugador

    public Vector2 offset; // Offset opcional para ajustar la posición de la cámara

    void Update()
    {
        // Verificar si hay un jugador asignado
        if (jugador != null)
        {
            // Obtener la posición actual de la cámara
            Vector3 posicionActual = transform.position;

            // Obtener la posición objetivo de la cámara
            Vector3 posicionObjetivo = new Vector3(jugador.position.x + offset.x, jugador.position.y + offset.y, posicionActual.z);

            // Asignar la posición objetivo a la cámara
            transform.position = posicionObjetivo;
        }
    }
}
