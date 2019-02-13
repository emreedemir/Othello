using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    private Piece[,] pieces;
    private List<Piece> m_listOfPieceS = new List<Piece>();
    Board board;



    virtual protected void Start()
    {
        pieces = new Piece[8, 8];
    }

    public void AddPiece(Piece piece, int xIndex, int yIndex)
    {
        Debug.Log("CAlisyor Add piece");
        pieces[xIndex, yIndex] = piece;
       
        board.SetToBoard(piece,xIndex,yIndex);
        m_listOfPieceS.Add(piece);
       
    }

    public void RemovePiece(Piece piece, int xIndex, int yIndex)
    {
       
        pieces[xIndex, yIndex] = null;
        m_listOfPieceS.Remove(piece);
        
    }
}
