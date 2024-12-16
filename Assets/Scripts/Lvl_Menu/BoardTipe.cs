using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BoardTipe : MonoBehaviour
{
    public enum BoardType { Melee, Ranged, Engineer }
    public BoardType boardType;

    [SerializeField] private ClassSelector classSelector; 

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            
            switch (boardType)
            {
                case BoardType.Melee:
                    classSelector.selectMelee();
                    break;
                case BoardType.Ranged:
                    classSelector.selectRanged();
                    break;
                case BoardType.Engineer:
                    classSelector.selectEngineer();
                    break;
            }
            
        }
    }
}

