using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    private Piece[,] pieces;
    private List<Piece> m_listOfPieceS = new List<Piece>();

    virtual protected void Start()
    {
        pieces = new Piece[8, 8];
    }

    public void AddPiece(Piece piece, int xIndex, int yIndex)
    {
        pieces[xIndex, yIndex] = piece;
        m_listOfPieceS.Add(piece);
    }

    public void RemovePiece(Piece piece, int xIndex, int yIndex)
    {
        pieces[xIndex, yIndex] = null;
        m_listOfPieceS.Remove(piece);
    }
}
