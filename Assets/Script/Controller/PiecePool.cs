using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePool : MonoBehaviour
{
    List<Piece> piecesBlack;
    List<Piece> piecesWhite;
    Position poolPosition = new Position(100, 100);

    public PieceView pieceView;

    public static int Piece_Count = 64;
    public void Awake()
    {
        piecesBlack = new List<Piece>(Piece_Count);
        piecesWhite = new List<Piece>(Piece_Count);
    }

    public void Start()
    {
        SetPool(PieceType.BlackPiece);
        SetPool(PieceType.WhitePiece);
    }
    public void SetPool(PieceType pieceType)
    {
        for (int i = 0; i < Board.BOARD_WIDTH; i++)
        {
            for (int j = 0; j < Board.BOARD_HEIGHT; j++)
            {
                if (pieceType == PieceType.BlackPiece)
                {
                    Piece piece = new Piece(pieceType, poolPosition);
                    piecesBlack.Add(piece);
                    PieceView pv = Instantiate(pieceView);
                    pv.SetPieceView(piece);
                }
                else
                {
                    Piece piece = new Piece(pieceType, poolPosition);
                    piecesWhite.Add(piece);
                    PieceView pv = Instantiate(pieceView);
                    pv.SetPieceView(piece);

                }
            }
        }
    }

    Piece TakePiece(PlayerType playerType)
    {
        if (playerType == PlayerType.Computer)
        {
            Piece piece = piecesBlack[0];
            piecesBlack.RemoveAt(0);
            return piece;
        }
        else
        {
            Piece piece = piecesWhite[0];
            piecesBlack.RemoveAt(0);
            return piece;
        }
    }

    public void Add(Piece piece)
    {
        if (piece.PieceType == PieceType.BlackPiece)
        {
            piecesBlack.Add(piece);
        }
        else
        {
            piecesWhite.Add(piece);
        }
    }

}
