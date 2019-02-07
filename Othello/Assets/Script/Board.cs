using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {



    public Cell cell;

    List<Cell> cells = new List<Cell>();

    
    int b_width = 8;
    int b_height =8;

    Cell[,] allCells;
     

    public void Start()
    {
 
        allCells = new Cell[b_width,b_height];

        CreateBoard();

    }

    public void CreateBoard()
    {
        int f_x = 0;
        for (int i = 0; i < b_width; i++)
        {
            for (int j = 0; j < b_height; j++)
            {
                Cell cellInstantie = Instantiate(cell, Vector3.zero, Quaternion.identity);
                
                allCells[i, j] = cellInstantie;
                cellInstantie.transform.name = "Cell[" + i + "," + j + "]";
                f_x++;
                cellInstantie.SetCell(i, j);
            }
        }

    }




    
 
}
