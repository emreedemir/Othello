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
            instantiePiece.pieceType =pieceType;
            instantiePiece.transform.name = "Piece[" + i + "]"+pieceType.ToString();
            pieceList.Add(instantiePiece);
        }
    }
}







