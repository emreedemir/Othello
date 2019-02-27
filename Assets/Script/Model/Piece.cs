using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece
{
    public PieceType PieceType { get; set; }
    public Position PiecePosition { get; set; }

    public Piece(PieceType pieceType, Position piecePosition)
    {
        this.PieceType = pieceType;
        this.PiecePosition = piecePosition;
    }
    public void SetPosition(Position position)
    {
        this.PiecePosition = position;
    }


}
