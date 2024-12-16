using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassSelectorMenuu : MonoBehaviour
{
    [SerializeField] private ClassSelector classSelector; 

    private BoardTipe currentBoard; 

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.F) && currentBoard != null)
        {     
            switch (currentBoard.boardType)
            {
                case BoardTipe.BoardType.Melee:
                    classSelector.selectMelee();
                    break;
                case BoardTipe.BoardType.Ranged:
                    classSelector.selectRanged();
                    break;
                case BoardTipe.BoardType.Engineer:
                    classSelector.selectEngineer();
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        BoardTipe board = collision.GetComponent<BoardTipe>();
        if (board != null)
        {

            currentBoard = board; 
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        BoardTipe board = collision.GetComponent<BoardTipe>();
        if (board != null && board == currentBoard)
        {
            currentBoard = null;
        }
    }
}