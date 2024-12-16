using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingEnemy : MonoBehaviour
{
    public Transform target; 
    public float personalSpaceRadius = 2.0f; 
    public LayerMask enemyLayer; 
    public float separationStrength = 1.0f; 
    public float seekStrength = 5.0f;

    private Vector2 velocity;

    void Update()
    {
        Vector2 seekForce = Seek(target.position); 
        Vector2 separationForce = Separation();

        
        Vector2 combinedForce = seekForce + separationForce;

      
        Move(combinedForce);
    }

    
    private Vector2 Seek(Vector2 targetPosition)
    {
        Vector2 desiredVelocity = (targetPosition - (Vector2)transform.position).normalized * seekStrength;
        return desiredVelocity;
    }

   
    private Vector2 Separation()
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, personalSpaceRadius, enemyLayer);
        Vector2 separationForce = Vector2.zero;

        foreach (Collider2D enemy in nearbyEnemies)
        {
            if (enemy.transform != transform)
            {
                Vector2 directionAway = (Vector2)(transform.position - enemy.transform.position);
                float distance = directionAway.magnitude;
                
                separationForce += directionAway.normalized / distance;
            }
        }

       
        return separationForce * separationStrength;
    }

    private void Move(Vector2 force)
    {
        velocity = force * Time.deltaTime;
        transform.position += (Vector3)velocity;
    }

    private void OnDrawGizmos()
    {
  
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, personalSpaceRadius);
    }
}