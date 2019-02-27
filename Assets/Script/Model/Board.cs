using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public static int BOARD_WIDTH = 8;
    public static int BOARD_HEIGHT = 8;

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
            }
        }
    }
}
