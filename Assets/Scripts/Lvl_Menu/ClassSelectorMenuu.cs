using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassSelectorMenuu : MonoBehaviour
{
    [SerializeField] private ClassSelector classSelector; // Referencia al sistema de selección de clase

    private BoardTipe currentBoard; // El cartel en el que el jugador está actualmente

    private void Update()
    {
        // Detecta si el jugador presiona "F"
        if (Input.GetKeyDown(KeyCode.F) && currentBoard != null)
        {

            Debug.Log($"Hola??");

            // Cambia la clase según el tipo del cartel actual
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

            Debug.Log($"Clase seleccionada: {currentBoard.boardType}");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el jugador está cerca de un cartel
        BoardTipe board = collision.GetComponent<BoardTipe>();
        if (board != null)
        {

            currentBoard = board; // Guarda la referencia al cartel actual
            Debug.Log($"Cerca de: {board.boardType}");
        }

        Debug.Log($"choca");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Limpia la referencia al salir del rango del cartel
        BoardTipe board = collision.GetComponent<BoardTipe>();
        if (board != null && board == currentBoard)
        {
            currentBoard = null;
            Debug.Log("Saliste del rango del cartel.");
        }
    }
}