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

        public readonly Position[] allDirections =
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
            x.unitBoardPosition == allDirections.ToList().
            Find(s => s.X + unit.unitBoardPosition.X == x.unitBoardPosition.X && s.Y + unit.unitBoardPosition.Y == x.unitBoardPosition.Y));
        }

        /// <summary>
        /// Use for 4 İnitial pieces position
        /// </summary>
        /// <param name="initialPieces"></param>
        public void SpawnInitialPiecesToBoard(Dictionary<Piece, Position> initialPieces)
        {
            foreach (KeyValuePair<Piece, Position> pair in initialPieces)
            {
                Unit unit = allUnitOfBoard.Find(x => x.unitBoardPosition == pair.Value);

                SpawnPieceToBoard(unit, pair.Key);
            }
        }

        public void SpawnPieceToBoard(Unit spawnUnit, Piece spawnedPiece)
        {
            spawnUnit.SetUnitPiece(spawnedPiece);

            spawnedPiece.SetPiecePositionAsVisual(spawnUnit.transform.position);

        }

        public void HandleUnitPress(Unit unit)
        {
            if (IsBoardPiecesTouchable)
            {
                Debug.Log("Unit touched");
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
