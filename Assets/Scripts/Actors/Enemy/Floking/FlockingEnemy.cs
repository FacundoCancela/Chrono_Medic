using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingEnemy : MonoBehaviour
{
    public Transform target; // El objetivo principal
    public float personalSpaceRadius = 2.0f; // Radio del área personal
    public LayerMask enemyLayer; // Capa que define qué son enemigos
    public float separationStrength = 1.0f; // Fuerza con la que los enemigos se repelen
    public float seekStrength = 5.0f; // Fuerza con la que siguen al objetivo

    private Vector2 velocity;

    void Update()
    {
        Vector2 seekForce = Seek(target.position); // Fuerza de seguir objetivo
        Vector2 separationForce = Separation(); // Fuerza de separación

        // Combinamos las dos fuerzas
        Vector2 combinedForce = seekForce + separationForce;

        // Aplicamos la dirección del movimiento al enemigo
        Move(combinedForce);
    }

    // Función de seguimiento del objetivo (Seek)
    private Vector2 Seek(Vector2 targetPosition)
    {
        Vector2 desiredVelocity = (targetPosition - (Vector2)transform.position).normalized * seekStrength;
        return desiredVelocity;
    }

    // Función de separación de otros enemigos
    private Vector2 Separation()
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, personalSpaceRadius, enemyLayer);
        Vector2 separationForce = Vector2.zero;

        foreach (Collider2D enemy in nearbyEnemies)
        {
            if (enemy.transform != transform) // Asegurarse de no contar a sí mismo
            {
                Vector2 directionAway = (Vector2)(transform.position - enemy.transform.position);
                float distance = directionAway.magnitude;
                // Cuanto más cerca el enemigo, más fuerte es la fuerza de separación
                separationForce += directionAway.normalized / distance;
            }
        }

        // Ajustar la fuerza de separación
        return separationForce * separationStrength;
    }

    // Aplicar movimiento
    private void Move(Vector2 force)
    {
        velocity = force * Time.deltaTime;
        transform.position += (Vector3)velocity;
    }

    private void OnDrawGizmos()
    {
        // Dibuja el área personal
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, personalSpaceRadius);
    }
}