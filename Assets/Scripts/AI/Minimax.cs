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
            int gameFinishedScore = EvaluateBoard(currentBoard, currentPlayerPieceType, oppositePlayerPieceType);

            if (gameFinishedScore == 10 || gameFinishedScore == -10)
            {
                return gameFinishedScore;
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
                    currentBoard[i].SetUnitPiece(PiecePoll.GetPiece(currentPlayerPieceType));

                    int bestValue = Minimax(currentBoard, currentPlayerPieceType, oppositePlayerPieceType, currentDepth, maximumDepth, !isMax);

                    Piece piece = currentBoard[i].currentPiece;

                    PiecePoll.AddToPoll(piece);

                    currentBoard[i].SetUnitFree();

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

                for (int j = 0; j < currentBoard.Count; j++)
                {
                    currentBoard[j].SetUnitPiece(PiecePoll.GetPiece(oppositePlayerPieceType));

                    int bestValue = Minimax(currentBoard,currentPlayerPieceType,oppositePlayerPieceType,currentDepth,maximumDepth,!isMax);

                    Piece piece = currentBoard[j].currentPiece;

                    PiecePoll.AddToPoll(piece);

                    currentBoard[j].SetUnitFree();

                    if (bestValue > best)
                    {
                        best = bestValue;
                    }
                }

                return best;
            }

        }


        public Unit GetBestPiece(List<Unit> boardPieces, PieceType currentPlayerPieceType, PieceType oppositePlayerPieceType, int maximumDepth)
        {
            Unit bestUnit = null;

            int bestScore = -1000;

            List<Unit> playablePieces = Board.GetPlayableUnits(currentPlayerPieceType, oppositePlayerPieceType, boardPieces);

            for (int j = 0; j < playablePieces.Count; j++)
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
    }
}
