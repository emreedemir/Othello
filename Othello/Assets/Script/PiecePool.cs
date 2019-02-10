using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePool : MonoBehaviour {

    public Piece m_piece;
    List<Piece> m_whitePieces = new List<Piece>();
    List<Piece> m_blackPieces = new List<Piece>();
    int m_countPiece = 64;

    Piece.PieceType blackType = Piece.PieceType.BlackPiece;
    Piece.PieceType whiteType = Piece.PieceType.WhitePiece;

    Vector3 poolPosition = new Vector3(100,100,100);

    
    public void Start()
    {
        InitiliazePool(m_whitePieces,whiteType);
        InitiliazePool(m_blackPieces,blackType);
    }

    public void InitiliazePool(List<Piece> pieceList,Piece.PieceType pieceType)
    {
        for (int i = 0; i < m_countPiece; i++)
        {
            Piece instantiePiece = Instantiate(m_piece,poolPosition,Quaternion.identity);
            instantiePiece.transform.name = "Piece[" + i + "]"+pieceType.ToString();
            instantiePiece.SetPiece(poolPosition,pieceType);
            instantiePiece.pieceType = pieceType;
            pieceList.Add(instantiePiece);
        }
    }

    public Piece TakeFromPool(Piece.PieceType pieceType)
    {
        Piece piece;
        if (pieceType == Piece.PieceType.BlackPiece)
        {
            piece = m_whitePieces[0];
            m_whitePieces.RemoveAt(1);
        }
        else if (pieceType == Piece.PieceType.WhitePiece)
        {
            piece = m_blackPieces[0];
            m_blackPieces.RemoveAt(1);
        }
        else
            piece = this.m_piece;

        return piece;
    }
   
    public void AddPool(Piece piece,Piece.PieceType pieceType)
    {

        if (pieceType == Piece.PieceType.BlackPiece)
        {
            m_blackPieces.Add(piece);
        }
        else if (pieceType == Piece.PieceType.WhitePiece)
        {
            m_whitePieces.Add(piece);
        }
    }



    public void ResetPool()
    {



    }


}







