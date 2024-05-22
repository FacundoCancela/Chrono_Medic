using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePoint : MonoBehaviour
{
    public int experience;

    public void ExperienceDrop(int experienceDroped)
    {
        experience = experienceDroped;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ExperienceManager experienceManager = FindObjectOfType<ExperienceManager>();
            if (experienceManager != null)
            {
                experienceManager.gainExperience(experience);
                Destroy(gameObject);
            }                         
        }
    }
}
