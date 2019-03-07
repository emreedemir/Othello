using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    BoardController boardController;

    Board board;

    public Player playerPrefab;
    public AIPlayer aiPlayerPrefab;


    void Awake()
    {
        board = new Board();
        boardController = FindObjectOfType<BoardController>();
    }
    private void Start()
    {
        Player player = Instantiate(playerPrefab);
        AIPlayer aiPlayer = Instantiate(aiPlayerPrefab);
        SetBoard();
        if (player != null && aiPlayer != null)
        {
            boardController.SetPlayers(player, aiPlayer, PieceType.BlackPiece, PieceType.WhitePiece);
        }

    }
    public void SetBoard()
    {
        board.InitBoard();
        boardController.ViewBoard(board);

    }
}
