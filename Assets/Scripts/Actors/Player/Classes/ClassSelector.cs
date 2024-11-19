using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ClassSelector : MonoBehaviour
{
    [SerializeField] SceneChanger sceneChanger;
    [SerializeField] ClassManager classManager;
    [SerializeField] List<TextMeshProUGUI> classTexts; // Asigna los textos de las clases en el orden Melee, Ranged, Engineer
        
    public string sceneName;

    private void Update()
    {
        MeleeText();
        DistanceText();
        EngineerText();
    }

    public void enterTheLevel()
    {
        sceneChanger.CambiarEscena(sceneName);
    }

    public void selectMelee()
    {
        classManager.SetCurrentClass(ClassManager.SelectedClass.Melee);
    }

    public void selectRanged()
    {
        classManager.SetCurrentClass(ClassManager.SelectedClass.Ranged);
    }

    public void selectEngineer()
    {
        classManager.SetCurrentClass(ClassManager.SelectedClass.Engineer);
    }

    public void MeleeText()
    {
        ClassStats meleeStats = GetClassStats(ClassManager.SelectedClass.Melee);
        classTexts[0].text = $"Health: {meleeStats.maxHealth}\n" +
                             $"Damage Multiplier: {meleeStats.damageMultiplier}\n" +
                             $"Movement Speed: {meleeStats.movementSpeed}\n";
    }

    public void DistanceText()
    {
        ClassStats rangedStats = GetClassStats(ClassManager.SelectedClass.Ranged);
        classTexts[1].text = $"Health: {rangedStats.maxHealth}\n" +
                             $"Damage Multiplier: {rangedStats.damageMultiplier}\n" +
                             $"Movement Speed: {rangedStats.movementSpeed}\n";
    }

    public void EngineerText()
    {
        ClassStats engineerStats = GetClassStats(ClassManager.SelectedClass.Engineer);
        classTexts[2].text = $"Health: {engineerStats.maxHealth}\n" +
                             $"Damage Multiplier: {engineerStats.damageMultiplier}\n" +
                             $"Movement Speed: {engineerStats.movementSpeed}\n";
    }

    private ClassStats GetClassStats(ClassManager.SelectedClass selectedClass)
    {
        foreach (ClassStats stats in classManager.stats)
        {
            if (stats.selectedClass == selectedClass)
            {
                return stats;
            }
        }
        return null;
    }
    
}
