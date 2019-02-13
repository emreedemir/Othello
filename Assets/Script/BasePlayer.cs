using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    private IDictionary<Position, Piece> pieceDictionary;
    private List<Piece> m_listOfPieceS = new List<Piece>();
    Board board;

    virtual protected void Start()
    {
        pieceDictionary = new Dictionary<Position, Piece>();
    }

    public void AddPiece(Piece piece, Position position)
    {
        Debug.Log("CAlisyor Add piece");
        pieceDictionary[position] = piece;
       
        board.SetToBoard(piece, position);
        m_listOfPieceS.Add(piece);
    }

    public void RemovePiece(Piece piece, Position position)
    {
       
        pieceDictionary[position] = null;
        m_listOfPieceS.Remove(piece);        
    }
}
