using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUnlocked : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private List<Image> abilityImages; // Lista de imágenes de las habilidades
    [SerializeField] private List<Sprite> abilitySprites; // Lista de sprites de las habilidades
    [SerializeField] private Color unlockedColor = Color.white; // Color cuando la habilidad está desbloqueada
    [SerializeField] private Color lockedColor = Color.black; // Color cuando la habilidad está bloqueada

    void Update()
    {
        // Actualizar el color y el sprite de cada imagen basada en el booleano correspondiente
        for (int i = 0; i < abilityImages.Count; i++)
        {
            // Asignar el sprite correspondiente
            abilityImages[i].sprite = abilitySprites[i];

            // Determinar el color basado en si la habilidad está desbloqueada
            switch (i)
            {
                case 0:
                    // Ataque a distancia siempre desbloqueado
                    abilityImages[i].color = unlockedColor;
                    break;
                case 1:
                    abilityImages[i].color = playerStats.basicSlashUnlocked ? unlockedColor : lockedColor;
                    break;
                case 2:
                    abilityImages[i].color = playerStats.bigSlashUnlocked ? unlockedColor : lockedColor;
                    break;
                case 3:
                    abilityImages[i].color = playerStats.orbitalWeaponUnlocked ? unlockedColor : lockedColor;
                    break;
                default:
                    Debug.LogError("Index out of range in abilityImages.");
                    break;
            }
        }
    }
}
