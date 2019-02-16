using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

    public enum PieceType
    {
        DefaultPiece=0,
        WhitePiece =1,
        BlackPiece=2
    }

    public PieceType pieceType = PieceType.DefaultPiece;

    
    Color white = Color.white;
    Color black = Color.black;
    Color red = Color.red;//Default color

    Renderer renderer;
    Material material;

    public void SetPiece(Vector3 position,PieceType pieceType)
    {

        transform.Rotate(new Vector3(90,0,0));
        transform.position += new Vector3(0,0.02f,0);
        renderer = GetComponent<Renderer>();
        transform.position = position;
        this.pieceType = pieceType;
        SetColor();
    }


    public void SetColor()
    {
        if (pieceType == PieceType.BlackPiece)
        {
            renderer.material.color=black ;
        }
        else if (pieceType == PieceType.WhitePiece)
        {
            renderer.material.color = white;   
        }
        else
        {
            renderer.material.color = red;
        }
    }
}
