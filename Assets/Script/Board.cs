using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {



    public Cell cell;
    public Piece pieces;


    List<Cell> cells = new List<Cell>();

    //Board width and height
    int b_width = 8;
    int b_height =8;

    //Initial position of Pieces on board position
    int s_position_x = 3;
    int s_position_y = 3;

    //All cells
    Cell[,] allCells;

    public void Start()
    {
        allCells = new Cell[b_width,b_height];
        CreateBoard();
       
    }

    public void CreateBoard()
    {
        for (int i = 0; i < b_width; i++)
        {
            for (int j = 0; j < b_height; j++)
            {
                Cell cellInstantie = Instantiate(cell, Vector3.zero, Quaternion.identity);
                allCells[i, j] = cellInstantie;
                cellInstantie.transform.name = "Cell[" + i + "," + j + "]";
                cellInstantie.SetCell(i, j);
            }
        }
    }
  
}
