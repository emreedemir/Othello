using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Position CellPosition { get; set; }
    public State CellState { get; set; }
    public Cell(Position cellPosition)
    {
        this.CellPosition = cellPosition;
    }
}
