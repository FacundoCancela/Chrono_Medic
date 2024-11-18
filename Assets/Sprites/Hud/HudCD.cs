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
                    abilityCooldown(swordAttack._timeSinceLastSlashAttack, images[i], texts[i]);
                    break;
                case "Range":
                    abilityCooldown(rangedAttack._timeSinceLastRangedAttack,images[i], texts[i]);
                    break;
                case "Engineer":
                    abilityCooldown(orbeAttack._timeSinceLastOrbitalAttack,images[i], texts[i]);
                    break;
                case "Curve":
                    abilityCooldown(curveAttack._timeSinceLastCurveAttack,images[i], texts[i]);
                    break;
                case "Boomerang":
                    abilityCooldown(boomerangAttack._timeSinceLastBoomerangAttack,images[i], texts[i]);
                    break;
            }
        }
        
        
    }

    private void abilityCooldown(float currentCooldown,Image skillImage, TextMeshProUGUI skillText)
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
                skillImage.fillAmount = currentCooldown;
            }
            if (skillText != null)
            {
                skillText.text = Mathf.Ceil(currentCooldown).ToString();
            }
        }
    }
}
