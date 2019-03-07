using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public CellView cellView;

    Cell[,] cells;

    BasePlayer Player1 { get; set; }
    BasePlayer Player2 { get; set; }

    PiecePool PiecePoll;
    public void ViewBoard(Board board)
    {

        cells = board.Cells;
        SetCellView();
    }

    public void SetCellView()
    {
        for (int i = 0; i < this.cells.GetLength(0); i++)
        {
            for (int j = 0; j < this.cells.GetLength(1); j++)
            {
                CellView cell = Instantiate(cellView);
                cell.SetCell(cells[i, j].CellPosition);
            }
        }
    }

    public void SetPlayers(BasePlayer player1, BasePlayer player2, PieceType pT1, PieceType pT2)
    {
        this.PiecePoll = FindObjectOfType<PiecePool>();
        this.Player1 = player1;
        this.Player1.SetPlayer(pT1);
        this.Player2 = player2;
        this.Player2.SetPlayer(pT2);
        SetInitStateBoard();
    }

    public void SetInitStateBoard()
    {
        Debug.Log(Player1.ToString());
        Debug.Log(Player2.ToString());
        if (Player1.PieceType == PieceType.BlackPiece)
        {
            Player1.pieces.Add(PiecePoll.TakePiece(Player1.PieceType), Board.firstBlack);
            Player1.pieces.Add(PiecePoll.TakePiece(Player1.PieceType), Board.secondBlack);
            Player2.pieces.Add(PiecePoll.TakePiece(Player2.PieceType), Board.firstWhite);
            Player2.pieces.Add(PiecePoll.TakePiece(Player2.PieceType), Board.secondWhite);
        }
        else
        {
            Player1.pieces.Add(PiecePoll.TakePiece(Player1.PieceType), Board.firstWhite);
            Player1.pieces.Add(PiecePoll.TakePiece(Player1.PieceType), Board.secondWhite);
            Player2.pieces.Add(PiecePoll.TakePiece(Player2.PieceType), Board.firstBlack);
            Player2.pieces.Add(PiecePoll.TakePiece(Player2.PieceType), Board.secondBlack);

        }
    }
}
