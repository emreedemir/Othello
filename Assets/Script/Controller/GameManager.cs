using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    BoardController boardController;

    Board board;

    

    void Awake()
    {
        board = new Board();
        boardController = FindObjectOfType<BoardController>();
    }
    private void Start()
    {
        SetBoard();
    }
    public void SetBoard()
    {
        board.InitBoard();
        boardController.ViewBoard(board);
    }
 
}
