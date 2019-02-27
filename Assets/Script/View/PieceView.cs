using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceView : MonoBehaviour
{
    Color colorBlack = Color.black;
    Color colorWhite = Color.white;

    Renderer renderer;

    public void Awake()
    {
        renderer = GetComponent<Renderer>();
    }
    public void SetPieceView(Piece piece)
    {
        transform.position = new Vector3(piece.PiecePosition.X,piece.PiecePosition.Y);

        if (piece.PieceType == PieceType.BlackPiece)
            renderer.material.color = colorBlack;
        else
            renderer.material.color = colorWhite;
    }
    public void SetPosition(Position position)
    {
        transform.position = new Vector3(position.X, 0,position.Y);
    }
}
