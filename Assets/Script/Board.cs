using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {



    [SerializeField] private Cell cellPrefab;
    public Piece pieces;


    List<Cell> cells = new List<Cell>();

    //Board width and height
    int width = 8;
    int height =8;

    //Initial position of Pieces on board position
    int s_position_x = 3;
    int s_position_y = 3;

    //All cells
    private IDictionary<Position, Cell> cellDictionary;

    public void Start()
    {
        cellDictionary = new Dictionary<Position, Cell>(width * height);
        CreateBoard();
       
    }

    public void CreateBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = Instantiate(cellPrefab, Vector3.zero, Quaternion.identity);
                Position position = new Position(x, y);
                cellDictionary.Add(position, cell);
                cell.transform.name += position.ToString();
                cell.SetCell(position);
            }
        }
    }

    public void SetToBoard(Piece piece, Position position)
    {
        Debug.Log("CAlisiyorr");
        piece.transform.position = cellDictionary[position].transform.position;

    }


}
