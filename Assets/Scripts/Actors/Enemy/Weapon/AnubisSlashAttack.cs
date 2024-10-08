using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnubisSlashAttack : MonoBehaviour
{
    [SerializeField] private GameObject pivot; // Punto de pivote (Anubis)
    [SerializeField] private float slashSpeed = 5f; // Velocidad del barrido
    [SerializeField] private float orbitalRadius = 2f; // Distancia del barrido desde el pivot

    private float currentAngle = 90f; // Iniciar el ángulo a 90 grados (frente a Anubis)

    private void Awake()
    {
        pivot = FindAnyObjectByType<BossModel>().gameObject; // Buscar a Anubis como el pivote
    }

    private void Update()
    {
        // Incrementar el ángulo hasta alcanzar 270 grados
        if (currentAngle < 450f)
        {
            currentAngle += slashSpeed * Time.deltaTime;

            // Calcular la posición en función del ángulo
            float x = Mathf.Cos(currentAngle * Mathf.Deg2Rad) * orbitalRadius;
            float y = Mathf.Sin(currentAngle * Mathf.Deg2Rad) * orbitalRadius;
            transform.position = pivot.transform.position + new Vector3(x, y, 0);

            // Apuntar hacia el pivote (Anubis)
            Vector3 direction = pivot.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 180f, Vector3.forward);
        }
        else
        {
            Destroy(gameObject); // Destruir el ataque al completar el barrido
        }
    }
}
