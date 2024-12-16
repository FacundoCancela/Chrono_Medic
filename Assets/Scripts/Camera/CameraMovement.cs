using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform jugador; 

    public Vector2 offset; 

    void Update()
    {
        
        if (jugador != null)
        {
           
            Vector3 posicionActual = transform.position;

           
            Vector3 posicionObjetivo = new Vector3(jugador.position.x + offset.x, jugador.position.y + offset.y, posicionActual.z);

            
            transform.position = posicionObjetivo;
        }
    }
}
