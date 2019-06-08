using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Unit : MonoBehaviour
{
    public Position UnitPosition { get; set; }
    public Action<Unit> OnClicked;
    public Piece CurrentPiece;

    List<Unit> neighbours = new List<Unit>();


    public void SetPosition(Position position)
    {
        UnitPosition = position;
        transform.position = new Vector2(UnitPosition.X,UnitPosition.Y);
        transform.localScale *= 0.95f;
        neighbours = Board.Instance.GetNeighbors(this);
    }

    public void SetPieceToUnit(Piece piece)
    {
        CurrentPiece = piece;
        CurrentPiece.SetPiecePosition(UnitPosition);
    }

    private void OnMouseDown()
    {
        if (OnClicked != null)
        {
            OnClicked.Invoke(this);
        }
    }
}
