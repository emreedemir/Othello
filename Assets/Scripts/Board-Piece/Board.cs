using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Board : MonoBehaviour
{
    public static Board Instance = new Board();
    public const int EDGE_UNIT_COUNT = 8;

    public Transform BoardPool;

    public Unit unitPref;

    public Unit[,] BoardSize = new Unit[EDGE_UNIT_COUNT, EDGE_UNIT_COUNT];

    public static readonly Position[] allDirections =
    {
        new Position(0,1),
        new Position(1,1),
        new Position(1,0),
        new Position(1,-1),
        new Position(0,-1),
        new Position(-1,-1),
        new Position(-1,0),
        new Position(-1,1)
    };


    public static readonly Position[] BlackPieceInitPositions =
    {
        new Position(3,3),
        new Position(4,4)
    };

    public static readonly Position[] WhitePieceInitPositions =
    {
        new Position(3,4),
        new Position(4,3),
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        CreateBoard();
    }

    public void Initialize(BasePlayer player_01, BasePlayer player_02)
    {
        if (player_01.playerPieceType == GameController.PieceType.Black)
        {
            foreach (Position pos in BlackPieceInitPositions)
            {

                Unit unit = BoardSize[pos.X, pos.Y];
                SetPieceToBoard(player_01.GetPiece(), unit);
            }
            foreach (Position pos in WhitePieceInitPositions)
            {
                Unit unit = BoardSize[pos.X, pos.Y];
                SetPieceToBoard(player_02.GetPiece(), unit);
            }
        }
        else if (player_01.playerPieceType == GameController.PieceType.White)
        {
            foreach (Position pos in WhitePieceInitPositions)
            {
                Unit unit = BoardSize[pos.X, pos.Y];
                SetPieceToBoard(player_01.GetPiece(), unit);
            }
            foreach (Position pos in BlackPieceInitPositions)
            {
                Unit unit = BoardSize[pos.X, pos.Y];
                SetPieceToBoard(player_02.GetPiece(), unit);
            }
        }
    }

    public void CreateBoard()
    {
        for (int i = 0; i < BoardSize.GetLength(0); i++)
        {
            for (int j = 0; j < BoardSize.GetLength(1); j++)
            {
                Unit unit = Instantiate(unitPref);
                unit.transform.SetParent(this.transform, true);
                unit.OnClicked += OnUnitClicked;
                unit.SetPosition(new Position(i, j));
                unit.transform.name = "Unit" + unit.UnitPosition.ToString();
                BoardSize[i, j] = unit;
            }
        }
    }

    public void OnUnitClicked(Unit unit)
    {
        GameController.Instance.EvaluateMove(unit);
    }

    public void SearchPieces()
    {
        //Horizontal Search 
        for (int i = 0; i < BoardSize.GetLength(1); i++)
        {
            SearchHorizontal(BoardSize[0, i]);
        }
        //Vertical Search
        for (int k = 0; k < BoardSize.GetLength(0); k++)
        {
            SearchVertical(BoardSize[k, 0]);
        }

        //Search Cross
        for (int j = 0; j < BoardSize.GetLength(1); j++)
        {
            SearchCrossLeft(BoardSize[0, j]);
        }
        for (int k = 0; k < BoardSize.GetLength(1); k++)
        {

        }
    }

    public void SearchCrossLeft(Unit unit)
    {
        List<Unit> convertableUnit = new List<Unit>();
        convertableUnit.Clear();

        Unit firstUnit = unit;

        if (firstUnit.CurrentPiece == null)
        {
            if (IsWithinBounds(new Position(unit.UnitPosition.X + 1, unit.UnitPosition.Y + 1)))
            {
                SearchCrossLeft(BoardSize[unit.UnitPosition.X + 1, unit.UnitPosition.Y + 1]);
            }
            return;
        }
        else
        {
            if (IsWithinBounds(new Position(firstUnit.UnitPosition.X + 1, firstUnit.UnitPosition.Y + 1)))
            {
                for (int j = firstUnit.UnitPosition.X + 1; j < BoardSize.GetLength(0); j++)
                {

                    if (BoardSize[j, j].CurrentPiece == null)
                    {
                        if (IsWithinBounds(new Position(j + 1, j + 1)))
                        {
                            SearchCrossLeft(BoardSize[j + 1, j + 1]);
                        }
                        return;
                    }
                    else if (BoardSize[j, j].CurrentPiece.Type == firstUnit.CurrentPiece.Type)
                    {
                        if (convertableUnit.Count > 0)
                        {
                            Convert(convertableUnit);
                            SearchCrossLeft(BoardSize[j, j]);
                        }
                        else
                        {
                            SearchCrossLeft(BoardSize[j, j]);
                        }
                    }
                    else if (BoardSize[j, j].CurrentPiece.Type != firstUnit.CurrentPiece.Type)
                    {
                        BoardSize[j, j].GetComponent<SpriteRenderer>().color = Color.blue;
                        convertableUnit.Add(BoardSize[j, j]);
                    }

                }
            }
        }
    }

    public void SearchCrossRight(Unit unit)
    {

    }



    public void SearchHorizontal(Unit unit)
    {
        List<Unit> convertableUnit = new List<Unit>();
        convertableUnit.Clear();

        Unit firstUnit = unit;

        if (firstUnit.CurrentPiece == null)
        {
            if (IsWithinBounds(new Position(unit.UnitPosition.X + 1, unit.UnitPosition.Y)))
            {
                SearchHorizontal(BoardSize[unit.UnitPosition.X + 1, unit.UnitPosition.Y]);
            }
            return;
        }
        else
        {
            if (IsWithinBounds(new Position(firstUnit.UnitPosition.X + 1, firstUnit.UnitPosition.Y)))
            {
                for (int j = unit.UnitPosition.X + 1; j < BoardSize.GetLength(0); j++)
                {
                    if (BoardSize[j, unit.UnitPosition.Y].CurrentPiece == null)
                    {
                        if (IsWithinBounds(new Position(j + 1, unit.UnitPosition.Y)))
                        {
                            SearchHorizontal(BoardSize[j + 1, unit.UnitPosition.Y]);
                        }
                        return;
                    }
                    else if (BoardSize[j, unit.UnitPosition.Y].CurrentPiece.Type == firstUnit.CurrentPiece.Type)
                    {

                        if (convertableUnit.Count > 0)
                        {
                            Convert(convertableUnit);
                            SearchHorizontal(BoardSize[j, unit.UnitPosition.Y]);
                        }
                        else
                        {
                            SearchHorizontal(BoardSize[j, unit.UnitPosition.Y]);
                        }
                    }
                    else if (BoardSize[j, unit.UnitPosition.Y].CurrentPiece.Type != firstUnit.CurrentPiece.Type)
                    {
                        convertableUnit.Add(BoardSize[j, unit.UnitPosition.Y]);
                    }
                }
            }
        }
    }

    public void SearchVertical(Unit unit)
    {
        List<Unit> convertableUnit = new List<Unit>();
        convertableUnit.Clear();

        Unit firstUnit = unit;
        Debug.Log(firstUnit.gameObject.name);
        if (firstUnit.CurrentPiece == null)
        {
            if (IsWithinBounds(new Position(firstUnit.UnitPosition.X, firstUnit.UnitPosition.Y + 1)))
            {
                SearchVertical(BoardSize[firstUnit.UnitPosition.X, firstUnit.UnitPosition.Y + 1]);
            }
            return;
        }
        else
        {
            if (IsWithinBounds(new Position(firstUnit.UnitPosition.X, firstUnit.UnitPosition.Y + 1)))
            {
                for (int j = unit.UnitPosition.Y + 1; j < BoardSize.GetLength(1); j++)
                {
                    if (BoardSize[unit.UnitPosition.X, j].CurrentPiece == null)
                    {
                        if (IsWithinBounds(new Position(unit.UnitPosition.X, j + 1)))
                        {
                            SearchVertical(BoardSize[unit.UnitPosition.X, j + 1]);
                        }
                        return;
                    }
                    else if (BoardSize[unit.UnitPosition.X, j].CurrentPiece.Type == firstUnit.CurrentPiece.Type)
                    {
                        if (convertableUnit.Count > 0)
                        {
                            Convert(convertableUnit);
                            SearchVertical(BoardSize[unit.UnitPosition.X, j]);
                        }
                        else
                        {
                            SearchVertical(BoardSize[unit.UnitPosition.X, j]);
                        }
                    }
                    else if (BoardSize[unit.UnitPosition.X, j].CurrentPiece.Type != firstUnit.CurrentPiece.Type)
                    {
                        convertableUnit.Add(BoardSize[unit.UnitPosition.X, j]);
                    }
                }
            }
        }
    }
    public void Convert(List<Unit> convert)
    {
        for (int i = 0; i < convert.Count; i++)
        {
            if (convert[i].CurrentPiece.Type != GameController.Instance.Human.playerPieceType)
            {
                GameController.Instance.Robot.AddPieceToPoll(convert[i].CurrentPiece);
                SetPieceToBoard(GameController.Instance.Human.GetPiece(), convert[i]);
            }
            else
            {
                GameController.Instance.Human.AddPieceToPoll(convert[i].CurrentPiece);
                SetPieceToBoard(GameController.Instance.Robot.GetPiece(), convert[i]);
            }
        }
    }

    public bool IsPlayable(Unit unit, GameController.PieceType pieceType)
    {
        List<Unit> units = GetNeighbors(unit);

        List<Unit> spawn = units.FindAll(x => x.CurrentPiece != null);

        Unit piece = spawn.Find(x => x.CurrentPiece.Type == pieceType);

        if (piece != null)
        {
            return true;
        }
        else
        {
            Debug.Log("You Cant Play this unit");
            return false;
        }
    }

    public void SetPieceToBoard(Piece piece, Unit unit)
    {
        unit.SetPieceToUnit(piece);
        piece.transform.SetParent(BoardPool, true);
        piece.gameObject.SetActive(true);
    }

    public void ClearBoard()
    {

    }

    public List<Unit> GetNeighbors(Unit unit)
    {
        List<Unit> neigbours = new List<Unit>();
        neigbours.Clear();

        foreach (Position pos in allDirections)
        {
            int newX = unit.UnitPosition.X + pos.X;
            int newY = unit.UnitPosition.Y + pos.Y;
            if (IsWithinBounds(new Position(newX, newY)))
            {
                neigbours.Add(BoardSize[newX, newY]);
            }
        }
        return neigbours;
    }

    bool IsWithinBounds(Position position)
    {
        return (position.X >= 0 && position.X < BoardSize.GetLength(0) && position.Y >= 0 && position.Y < BoardSize.GetLength(1));
    }
}
