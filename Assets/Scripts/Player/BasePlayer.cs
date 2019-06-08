using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayer : MonoBehaviour
{
    public const int PIECE_COUNT = 64;
    public Piece Piece;
    public List<Piece> PiecePool;
    public List<Piece> PiecesOnBoard;

    public GameController.PieceType playerPieceType;

    public void AddPieceToPoll(Piece piece)
    {
        piece.transform.SetParent(transform, true);
        piece.gameObject.SetActive(false);
        PiecePool.Add(piece);
    }

    public void CreatePiecePool()
    {
        for (int i = 0; i < PIECE_COUNT; i++)
        {
            Piece piece = Instantiate(Piece);
            piece.SetPiece(playerPieceType);
            piece.transform.SetParent(this.transform, true);
            piece.gameObject.SetActive(false);
            PiecePool.Add(piece);
        }
    }

    public Piece GetPiece()
    {
        if (PiecePool.Count > 0)
        {
            Piece piece = PiecePool[0];
            PiecePool.RemoveAt(0);
            return piece;
        }
        else
        {
            Debug.Log("No piece at pool");
            return null;
        }
    }

    public void InitiliazePlayer(GameController.PieceType pieceType)
    {
        {
            Debug.Log(gameObject.name + " Piece type" + playerPieceType.ToString());
            playerPieceType = pieceType;
            PiecePool = new List<Piece>();
            PiecesOnBoard = new List<Piece>();
            CreatePiecePool();
        }
    }
}
