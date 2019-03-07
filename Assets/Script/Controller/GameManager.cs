using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    BoardController boardController;

    Board board;

    public Player player;
    public AIPlayer aiPlayer;


    void Awake()
    {
        board = new Board();
        boardController = FindObjectOfType<BoardController>();
    }
    private void Start()
    {
        SetBoard();
        if (player != null && aiPlayer != null)
            SetPlayers(player, aiPlayer);
    }
    public void SetBoard()
    {
        board.InitBoard();
        boardController.ViewBoard(board);

    }
    public void SetPlayers(BasePlayer player1, BasePlayer player2)
    {
        board.InitFirstState(player1, player2);
    }
}
