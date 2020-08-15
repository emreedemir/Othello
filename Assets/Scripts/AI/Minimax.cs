using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Othello
{
    public class MinimaxAlgoritm
    {
        public const int MAX = 1000;

        public const int MIN = -1000;

        private bool IsEmptyUnit(List<Unit> currentBoard)
        {
            return currentBoard.Find(x => x.currentPiece == null);
        }

        private int Minimax(List<Unit> currentBoard, PieceType currentPlayerPieceType, PieceType oppositePlayerPieceType, int alpha, int beta, int currentDepth, bool isMax)
        {
            if (currentDepth == 0 || !IsEmptyUnit(currentBoard))
            {
                return currentBoard.FindAll(x => x.currentPiece != null).FindAll(x => x.currentPiece.pieceType == currentPlayerPieceType).Count();
            }

            if (isMax)
            {
                int best = MIN;

                //WARNING
                List<Unit> playableUnitForCurrentPlayer = Board.GetPlayableUnits(currentPlayerPieceType, oppositePlayerPieceType, currentBoard);

                for (int i = 0; i < playableUnitForCurrentPlayer.Count; i++)
                {
                    playableUnitForCurrentPlayer[i].SetUnitPiece(PiecePoll.GetPiece(currentPlayerPieceType));

                    int bestValue = Minimax(currentBoard, currentPlayerPieceType, oppositePlayerPieceType, alpha, beta, currentDepth - 1, !isMax);

                    Piece piece = playableUnitForCurrentPlayer[i].currentPiece;

                    PiecePoll.AddToPoll(piece);

                    playableUnitForCurrentPlayer[i].SetUnitFree();

                    best = Math.Max(best, bestValue);

                    alpha = Math.Max(alpha, best);

                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                return best;
            }
            else
            {
                int best = MAX;

                //WARNING
                List<Unit> playableUnitForOppositePlayer = Board.GetPlayableUnits(oppositePlayerPieceType, currentPlayerPieceType, currentBoard);

                for (int j = 0; j < playableUnitForOppositePlayer.Count; j++)
                {
                    playableUnitForOppositePlayer[j].SetUnitPiece(PiecePoll.GetPiece(oppositePlayerPieceType));

                    int bestValue = Minimax(currentBoard, currentPlayerPieceType, oppositePlayerPieceType, alpha, beta, currentDepth - 1, !isMax);

                    Piece piece = playableUnitForOppositePlayer[j].currentPiece;

                    PiecePoll.AddToPoll(piece);

                    playableUnitForOppositePlayer[j].SetUnitFree();

                    best = Math.Min(best, bestValue);

                    beta = Math.Min(beta, best);

                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                return best;
            }
        }

        public Unit GetBestPiece(List<Unit> boardPieces, PieceType currentPlayerPieceType, PieceType oppositePlayerPieceType, int maximumDepth)
        {
            Unit bestUnit = null;

            int bestScore = MIN;

            List<Unit> playablePieces = Board.GetPlayableUnits(currentPlayerPieceType, oppositePlayerPieceType, boardPieces);

            for (int j = 0; j < playablePieces.Count; j++)
            {

                playablePieces[j].SetUnitPiece(PiecePoll.GetPiece(currentPlayerPieceType));

                int score = Minimax(boardPieces, currentPlayerPieceType, oppositePlayerPieceType, MIN, MAX, maximumDepth, false);

                Piece piece = playablePieces[j].currentPiece;

                PiecePoll.AddToPoll(piece);

                playablePieces[j].SetUnitFree();

                if (score > bestScore)
                {
                    bestScore = score;

                    bestUnit = playablePieces[j];
                }
            }

            return bestUnit;
        }
    }
}
