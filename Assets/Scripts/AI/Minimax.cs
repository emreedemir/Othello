using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Othello
{
    public class MinimaxAlgoritm
    {
        private int EvaluateBoard(List<Unit> currentBoard, PieceType currentPlayerPieceType, PieceType oppositePlayerPieceType)
        {
            return 1;
        }

        private bool IsEmptyUnit(List<Unit> currentBoard)
        {
            return false;
        }

        public int Minimax(List<Unit> currentBoard, PieceType currentPlayerPieceType, PieceType oppositePlayerPieceType, int currentDepth, int maximumDepth, bool isMax)
        {
            int score = EvaluateBoard(currentBoard, currentPlayerPieceType, oppositePlayerPieceType);

            if (score == 10 || score == -10)
            {
                return score;
            }

            if (!IsEmptyUnit(currentBoard))
            {
                return 0;
            }

            if (isMax)
            {
                int best = -1000;

                for (int i = 0; i < currentBoard.Count; i++)
                {
                    //currentBoard[i].CurrentPiece == Set as currentPlayerPieceType;

                    int bestValue = Minimax(currentBoard, currentPlayerPieceType, oppositePlayerPieceType, currentDepth, maximumDepth, !isMax);

                    //currentBoard[i].CurrentPiece == Set as Empty

                    if (bestValue > best)
                    {
                        best = bestValue;
                    }

                }


                return best;
            }
            else
            {
                int best = 1000;

                return best;
            }

        }


        public Unit GetBestPiece(List<Unit> boardPieces, PieceType currentPlayerPieceType, PieceType oppositePlayerPieceType, int maximumDepth)
        {
            Unit bestUnit = null;

            int bestScore = -1000;

            List<Unit> playablePieces = GetPlayeblaPieces(boardPieces);

            for (int j = 0; j < boardPieces.Count; j++)
            {
                int currentDepth = 0;

                //board[i].CurrentPiece =//Set Piece

                int score = Minimax(boardPieces, currentPlayerPieceType, oppositePlayerPieceType, currentDepth, maximumDepth, false);

                if (score > bestScore)
                {
                    bestScore = score;

                    bestUnit = boardPieces[j];
                }

            }

            return bestUnit;
        }

        public static List<Unit> GetPlayeblaPieces(List<Unit> boardPieces)
        {
            return null;
        }
    }
}
