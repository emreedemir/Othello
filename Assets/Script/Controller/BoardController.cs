using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public CellView cellView;

    Cell[,] cells;

    public void ViewBoard(Board board)
    {
        cells = board.Cells;
        SetCellView();
    }

    public void SetCellView()
    {
        for (int i = 0; i < this.cells.GetLength(0); i++)
        {
            for (int j = 0; j < this.cells.GetLength(1); j++)
            {
                CellView cell = Instantiate(cellView);
                cell.SetCell(cells[i, j].CellPosition);
            }
        }
    }
}
