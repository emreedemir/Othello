using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Othello
{
    public class Board
    {
        private Unit unitPrefab;

        public Unit UnitPrefab
        {
            get
            {
                if (unitPrefab == null)
                {
                    unitPrefab = Resources.Load<Unit>("Prefab/unitPrefab");
                }
                return unitPrefab;
            }
        }

        public List<Unit> allUnitOfBoard;

        public readonly int BOARD_DIMENSION = 8;

        public Transform unitParent;

        public bool IsBoardPiecesTouchable;

        public Action<Board> EvaluateBoard;

        public static Position[] allDirections =
        {
            new Position(1,0),
            new Position(-1,0),
            new Position(0,1),
            new Position(0,-1),
            new Position(1,1),
            new Position(-1,1),
            new Position(-1,-1),
            new Position(1,-1)
        };

        public readonly Position[] initialWhitePiecesPositions =
        {
            new Position(4,3),
            new Position(3,4)
        };

        public readonly Position[] initialBlackPiecesPositions =
        {
            new Position(3,3),
            new Position(4,4)

        };

        /// <summary>
        /// Board created ,Dimension is 8 as default
        /// </summary>
        public void CreateBoard()
        {
            GameObject boardGameObject = new GameObject("Board");

            unitParent = boardGameObject.transform;

            allUnitOfBoard = new List<Unit>();

            for (int i = 0; i < BOARD_DIMENSION; i++)
            {
                for (int j = 0; j < BOARD_DIMENSION; j++)
                {
                    Unit newUnit = MonoBehaviour.Instantiate(UnitPrefab);

                    newUnit.transform.SetParent(unitParent);

                    newUnit.SetUnitPosition(new Position(i, j));

                    allUnitOfBoard.Add(newUnit);

                }
            }
        }

        public void SetUnitPressEvents(List<Unit> units)
        {
            units.ForEach(delegate (Unit unit)
            {
                unit.OnUnitPressed += HandleUnitPress;
            });

        }

        public void SetUnitNeigbours(List<Unit> boardUnits)
        {
            boardUnits.ForEach(delegate (Unit unit)
            {
                unit.unitNeigbours = FindUnitNeigbours(unit, boardUnits);
            });
        }

        public List<Unit> FindUnitNeigbours(Unit unit, List<Unit> allBoardUnits)
        {
            return allUnitOfBoard.FindAll(x =>
            x.unitBoardPosition == (unit.unitBoardPosition + allDirections.ToList().
            Find(s => s + unit.unitBoardPosition == x.unitBoardPosition)) && x.unitBoardPosition != unit.unitBoardPosition);
        }


        public void SpawnPieceToBoard(Unit spawnUnit, Piece spawnedPiece)
        {
            spawnUnit.SetUnitPiece(spawnedPiece);

            spawnedPiece.SetPiecePositionAsVisual(spawnUnit.transform.position);

            spawnedPiece.gameObject.SetActive(true);

        }

        public void HandleUnitPress(Unit unit)
        {
            if (IsBoardPiecesTouchable)
            {
                Debug.Log("Unit touched");
            }
            else
            {
                Debug.Log("Units Non Touchable");
            }
        }

        public static List<Unit> GetPlayableUnits(PieceType currentPlayerPieceType, PieceType oppositePlayerPieceType, List<Unit> boardUnits)
        {
            List<Unit> playableUnits = new List<Unit>();

            ///Found every Empty units

            ////WARNING
            ///
            /* Increased performance by selecting units with at least one full neighbor instead of searching for each empty unit to improve performance*/

            List<Unit> emptyUnits = boardUnits.FindAll(x => x.currentPiece == null);

            emptyUnits = emptyUnits.FindAll(x => x.unitNeigbours.Find(a => a.currentPiece != null));

            emptyUnits.ForEach(x => x.GetComponent<SpriteRenderer>().color = Color.blue);

            for (int i = 0; i < emptyUnits.Count; i++)
            {
                for (int j = 0; j < allDirections.Length; j++)
                {
                    bool playable = IsPlayable(emptyUnits[i], boardUnits, allDirections[j], currentPlayerPieceType, oppositePlayerPieceType, 0);

                    if (playable == true)
                    {
                        playableUnits.Add(emptyUnits[i]);
                    }
                }
            }

            return playableUnits;
        }

        private static bool IsPlayable(Unit currentUnit, List<Unit> allUnits, Position directionVector, PieceType currentPlayerPieceType, PieceType oppositePlayerPieceType, int depth)
        {
            Unit nextUnit = allUnits.Find(x => x.unitBoardPosition == currentUnit.unitBoardPosition + directionVector);


            if (nextUnit == null)
            {
                return false;
            }

            if (nextUnit.currentPiece == null)
            {
                return false;
            }

            if (nextUnit.currentPiece.pieceType == oppositePlayerPieceType)
            {
                return IsPlayable(nextUnit, allUnits, directionVector, currentPlayerPieceType, oppositePlayerPieceType, depth + 1);
            }

            if (nextUnit.currentPiece.pieceType == currentPlayerPieceType)
            {
                if (depth == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }
    }
    public enum PieceType
    {
        None,
        Black,
        White
    }

}
