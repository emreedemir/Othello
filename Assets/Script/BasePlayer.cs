using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    private IDictionary<Position, Piece> pieceDictionary = new Dictionary<Position, Piece>();
    private List<Piece> m_listOfPieceS = new List<Piece>();
    Board board;


    public void Awake()
    {
      board = FindObjectOfType<Board>();
    }

    virtual protected void Start()
    {
       
        
    }

    public void AddPiece(Piece piece, Position position)
    {
        
        pieceDictionary[position] = piece;
        Debug.Log("CAlisyor Add piece");
        board.SetToBoard(piece, position);
        m_listOfPieceS.Add(piece);
    }

    public void RemovePiece(Piece piece, Position position)
    {

        pieceDictionary[position] = null;
        m_listOfPieceS.Remove(piece);
    }
}
