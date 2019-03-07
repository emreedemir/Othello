using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class BasePlayer : MonoBehaviour
{
    
    public PieceType PieceType { get; set; }
    public IDictionary<Piece, Position> pieces;

    protected virtual void Start()
    {
        pieces = new Dictionary<Piece, Position>(64);
    }
    public virtual void SetPlayer(PieceType pieceType)
    {
        this.PieceType = pieceType;
    }
}
