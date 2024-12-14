using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HudCD : MonoBehaviour
{
    public List<Image> images = new List<Image>();
    public List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();

    public AbilityUnlocked abilityUnlocked;

    public SwordAttack swordAttack;
    public CurveAttack curveAttack;
    public BoomerangAttack boomerangAttack;
    public RangedAttack rangedAttack;
    public OrbeAttack orbeAttack;
    public ExperienceManager experienceManager;

    public void Start()
    {
        foreach (var image in images)
        {
            image.fillAmount = 0;
        }
        foreach (var text in texts)
        {
            text.text = "";
        }
    }
    public void Update()
    {
        for (int i = 0; i < abilityUnlocked.abilityObjects.Count; i++)
        {
            var spriteName = abilityUnlocked.abilityObjects[i].GetComponent<Image>().sprite.name;
            switch (spriteName)
            {
                case "Melee":
                    abilityCooldown(swordAttack._timeSinceLastSlashAttack, experienceManager.meleeCooldown, images[i], texts[i]);
                    break;
                case "Range":
                    abilityCooldown(rangedAttack._timeSinceLastRangedAttack, experienceManager.rangedCooldown, images[i], texts[i]);
                    break;
                case "Engineer":
                    abilityCooldown(orbeAttack._timeSinceLastOrbitalAttack,experienceManager.orbitalCooldown, images[i], texts[i]);
                    break;
                case "Curve":
                    abilityCooldown(curveAttack._timeSinceLastCurveAttack, experienceManager.curveSwordCooldown, images[i], texts[i]);
                    break;
                case "Boomerang":
                    abilityCooldown(boomerangAttack._timeSinceLastBoomerangAttack,experienceManager.boomerangCooldown, images[i], texts[i]);
                    break;
            }
        }   
    }

    private void abilityCooldown(float currentCooldown, float maxCooldown, Image skillImage, TextMeshProUGUI skillText)
    {
        if(currentCooldown <= 0f)
        {
            if (skillImage != null)
            {
                skillImage.fillAmount =0;
            }
            if (skillText != null)
            {
                skillText.text = "";
            }
        }
        else
        {
            if (skillImage != null)
            {
                skillImage.fillAmount = currentCooldown / maxCooldown;
            }
            if (skillText != null)
            {
                skillText.text = Mathf.Ceil(currentCooldown).ToString();
            }
        }
    }
}
