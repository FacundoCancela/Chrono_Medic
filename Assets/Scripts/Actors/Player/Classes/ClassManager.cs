using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassManager : MonoBehaviour
{
    [SerializeField] public List<ClassStats> stats = new List<ClassStats>();
    public enum SelectedClass
    {
        Melee,
        Ranged,
        Engineer,
    }

    public static SelectedClass currentClass;

    public void SetCurrentClass(SelectedClass selectedClass)
    {
        currentClass = selectedClass;
    }
    public SelectedClass GetCurrentClass()
    {
        return currentClass;
    }

}
