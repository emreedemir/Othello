using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Position PiecePosition { get; set; }


    public GameController.PieceType Type;

    public void SetPiece(GameController.PieceType pieceType)
    {
        Type = pieceType;

        if (Type == GameController.PieceType.Black)
        {
            GetComponent<SpriteRenderer>().color = Color.black;
        }
        else if(Type ==GameController.PieceType.White)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void SetPiecePosition(Position position)
    {
        PiecePosition = position;
        transform.position = new Vector3(position.X,position.Y,-1);
    }
}
