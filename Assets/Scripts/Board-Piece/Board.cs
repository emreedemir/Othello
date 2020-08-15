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

        public Action<Unit> EvaluateUserPress;


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
                EvaluateUserPress?.Invoke(unit);
            }
            else
            {
                Debug.Log("Board is Nontouchable");
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

        /// <summary>
        /// After Any player move a piece ,this method look for any turnable pieces and turn oppositePieces
        /// </summary>
        /// <param name="playedPiece"></param>
        public List<Unit> GetAllTurnableUnitPieces(Unit playedUnit, List<Unit> allUnits, PieceType playerPieceType, PieceType oppositePieceType)
        {
            List<Unit> allTurnableUnits = new List<Unit>();

            for (int i = 0; i < allDirections.Length; i++)
            {
                List<Piece> turnablePieces = new List<Piece>();

                if (IsTurnable(playedUnit, allDirections[i], playerPieceType, oppositePieceType, allUnits, turnablePieces, 0))
                {
                    allTurnableUnits.AddRange(allUnits.FindAll(x => turnablePieces.Find(a => a == x.currentPiece) && !turnablePieces.Find(b => b == x)));
                }
            }

            return allTurnableUnits;
        }

        private bool IsTurnable(Unit playedUnit, Position directionVector, PieceType firstPieceType, PieceType oppositePieceType, List<Unit> allUnits, List<Piece> turnablePieces, int currentDepth)
        {
            Unit nextUnit = allUnits.Find(x => x.unitBoardPosition == playedUnit.unitBoardPosition + directionVector);

            if (nextUnit != null)
            {
                if (nextUnit.currentPiece != null)
                {
                    if (nextUnit.currentPiece.pieceType == oppositePieceType)
                    {
                        turnablePieces.Add(nextUnit.currentPiece);

                        return IsTurnable(nextUnit, directionVector, firstPieceType, oppositePieceType, allUnits, turnablePieces, currentDepth + 1);
                    }
                    else
                    {
                        if (currentDepth == 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
    public enum PieceType
    {
        None,
        Black,
        White
    }

}
