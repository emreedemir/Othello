using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    BoardController boardController;

    Board board;

    PiecePool piecePool;

    void Awake()
    {
        piecePool = FindObjectOfType<PiecePool>();
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
