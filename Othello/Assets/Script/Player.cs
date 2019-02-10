using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player:MonoBehaviour  {


    Piece[,] pieces;

    List<Piece> m_listOfPieceS = new List<Piece>();
    public void Start()
    {
        pieces =new Piece[8,8];
    }

    public void AddPiece(Piece piece,int xIndex,int yIndex)
    {
        pieces[xIndex, yIndex] = piece;
        m_listOfPieceS.Add(piece);

    }

    public void RemovePiece(Piece piece,int xIndex,int yIndex)
    {
        pieces[xIndex, yIndex] = null;
        m_listOfPieceS.Remove(piece);

    }
}
