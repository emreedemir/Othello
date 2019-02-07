﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {



    int x_index;
    int y_index;

    float bias = .055f;

    public int c_value;
   
    Color c_green = Color.green;
    Color c_white = Color.white;

    Vector3 cellPosition;

    Vector3 scaleCell = new Vector3(5,5,5);

    Renderer renderer;

    public enum CellType
    {
        Empty =0,
        WhitePiece=1,
        BlackPiece=2
    }

    CellType cellType = CellType.Empty;

    public void SetCell(int x,int y)
    {
        x_index = x;
        y_index = y;

        renderer = GetComponent<Renderer>();
        cellPosition =bias*( new Vector3(x_index,0,y_index));
        transform.position = cellPosition;
        transform.localScale = scaleCell;

        renderer.material.color = c_green;

    }
}
