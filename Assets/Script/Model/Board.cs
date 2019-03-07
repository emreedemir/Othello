using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public static int BOARD_WIDTH = 8;
    public static int BOARD_HEIGHT = 8;

    #region Init Position of Pieces
    public static Position firstWhite = new Position { X = 3, Y = 3 };
    public static Position secondWhite = new Position { X = 4, Y = 4 };

    public static Position firstBlack = new Position { X = 4, Y = 3 };
    public static Position secondBlack = new Position { X = 3, Y = 4 };
    #endregion

    public Cell[,] Cells { get; set; }

    public void InitBoard()
    {
        Cells = new Cell[BOARD_WIDTH, BOARD_HEIGHT];
        SetCell();
    }

    public void SetCell()
    {
        for (int i = 0; i < BOARD_WIDTH; i++)
        {
            for (int j = 0; j < BOARD_HEIGHT; j++)
            {
                Position position = new Position(i, j);
                Cells[i, j] = new Cell(position);
                Cells[i, j].CellState = State.Null;
            }
        }
    }

}
