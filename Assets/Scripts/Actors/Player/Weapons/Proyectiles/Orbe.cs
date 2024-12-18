using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbe : MonoBehaviour
{
    [SerializeField] public GameObject pivot;
    [SerializeField] public ExperienceManager experienceManager;
    [SerializeField] public PlayerStats playerStats;

    private float currentAngle; // �ngulo actual del orbe alrededor del personaje
    private float orbitalRadius;

    private void Awake()
    {
        experienceManager = FindAnyObjectByType<ExperienceManager>();
        pivot = FindAnyObjectByType<PlayerPivot>().gameObject;
        orbitalRadius = Vector3.Distance(transform.position, pivot.transform.position); // Distancia inicial
    }

    private void Start()
    {
        Destroy(gameObject, experienceManager.orbitalDuration);
    }

    private void Update()
    {
        // Incrementar el �ngulo de rotaci�n de manera constante
        currentAngle += experienceManager.orbitalSpeed * Time.deltaTime;

        // Mantener la posici�n del orbe en funci�n del �ngulo
        float x = Mathf.Cos(currentAngle) * orbitalRadius;
        float y = Mathf.Sin(currentAngle) * orbitalRadius;
        transform.position = pivot.transform.position + new Vector3(x, y, 0);
    }

    // M�todo para establecer el �ngulo inicial del orbe
    public void SetInitialAngle(float initialAngle)
    {
        currentAngle = initialAngle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IEnemyController enemyController = collision.gameObject.GetComponent<IEnemyController>();
            if (enemyController != null)
            {
                enemyController.GetDamaged(playerStats.damageMultiplier * experienceManager.engineerDamage);
            }
        }
    }
}
