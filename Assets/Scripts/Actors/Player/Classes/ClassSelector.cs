using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassSelector : MonoBehaviour
{
    [SerializeField] SceneChanger sceneChanger;
    [SerializeField] ClassManager classManager;
    public string sceneName;

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

}
