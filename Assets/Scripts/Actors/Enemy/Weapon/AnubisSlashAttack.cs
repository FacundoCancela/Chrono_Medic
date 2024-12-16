using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnubisSlashAttack : MonoBehaviour
{
    [SerializeField] private GameObject pivot; 
    [SerializeField] private float slashSpeed = 5f; 
    [SerializeField] private float orbitalRadius = 2f; 

    private float currentAngle = 90f; 

    private void Awake()
    {
        pivot = FindAnyObjectByType<BossModel>().gameObject; 
    }

    private void Update()
    {
       
        if (currentAngle < 450f)
        {
            currentAngle += slashSpeed * Time.deltaTime;

            
            float x = Mathf.Cos(currentAngle * Mathf.Deg2Rad) * orbitalRadius;
            float y = Mathf.Sin(currentAngle * Mathf.Deg2Rad) * orbitalRadius;
            transform.position = pivot.transform.position + new Vector3(x, y, 0);

            
            Vector3 direction = pivot.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 180f, Vector3.forward);
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}
