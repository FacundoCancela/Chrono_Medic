using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour
{
    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public PlayerView playerView;
    [SerializeField] public ExperienceManager experienceManager;

    private void Awake()
    {
        experienceManager = FindAnyObjectByType<ExperienceManager>();
        playerView = FindAnyObjectByType<PlayerView>();
    }

    private void Start()
    {
        Destroy(gameObject, experienceManager.curveSwordDuration);
    }


    public void Update()
    {
        transform.parent = null;

        transform.position = playerView.transform.position;
        transform.localEulerAngles= new Vector3(0, 0, transform.localEulerAngles.z + experienceManager.curveSwordSpeed * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IEnemyController enemyController = collision.gameObject.GetComponent<IEnemyController>();
            ExperienceManager experienceManager = FindAnyObjectByType<ExperienceManager>();
            if (enemyController != null)
            {
                enemyController.GetDamaged(playerStats.damageMultiplier * experienceManager.curveSwordDamage);
            }
        }
    }
}
