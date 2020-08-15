using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Othello
{
    public class GameController : MonoBehaviour
    {
        public Human human;

        public Robot robot;

        public PiecePoll piecePool;

        public Board board;

        MinimaxAlgoritm minimaxAlgoritm;

        public Text humanScore;

        public Text robotScore;

        private void Start()
        {
            piecePool = new PiecePoll();

            piecePool.CreatePoll();

            board = new Board();

            board.CreateBoard();

            board.SetUnitNeigbours(board.allUnitOfBoard);

            board.SetUnitPressEvents(board.allUnitOfBoard);

            human = new Human();

            human.pieceType = PieceType.Black;

            robot = new Robot();

            robot.pieceType = PieceType.White;

            minimaxAlgoritm = new MinimaxAlgoritm();

            SpawnInitialPiecesAtBoard(human, robot, board);

            //This is a test
            List<Unit> playableUnitsForHuman = Board.GetPlayableUnits(human.pieceType, robot.pieceType, board.allUnitOfBoard);

            board.EvaluateUserPress += HandleUserTouchUnit;

            Turn(human);

        }

        public void HandleUserTouchUnit(Unit unit)
        {
            List<Unit> playableUnits = Board.GetPlayableUnits(human.pieceType, robot.pieceType, board.allUnitOfBoard);

            if (playableUnits.Find(x => x == unit))
            {

                Piece playerPiece = PiecePoll.GetPiece(human.pieceType);

                board.SpawnPieceToBoard(unit, playerPiece);

                TurnPiecesAfterPlayerPlayTurn(unit, human, robot);

                humanScore.text = "HUMAN SCORE : " + board.allUnitOfBoard.FindAll(x => x.currentPiece != null).FindAll(a => a.currentPiece.pieceType == human.pieceType).Count();

                robotScore.text = "ROBOT SCORE : " + board.allUnitOfBoard.FindAll(x => x.currentPiece != null).FindAll(a => a.currentPiece.pieceType == robot.pieceType).Count();

                Turn(robot);
            }
        }

        public void Turn(BasePlayer currentPlayer)
        {
            if (currentPlayer is Human)
            {

                board.IsBoardPiecesTouchable = true;
            }
            else if (currentPlayer is Robot)
            {
                board.IsBoardPiecesTouchable = false;


                Unit bestUnit = minimaxAlgoritm.GetBestPiece(board.allUnitOfBoard, currentPlayer.pieceType, human.pieceType, 2);

                Piece piece = PiecePoll.GetPiece(currentPlayer.pieceType);

                if (piece == null)
                {
                    FinishGame();

                    return;
                }


                board.SpawnPieceToBoard(bestUnit, piece);

                TurnPiecesAfterPlayerPlayTurn(bestUnit, currentPlayer, human);

                robotScore.text = "ROBOT SCORE : " + board.allUnitOfBoard.FindAll(x => x.currentPiece != null).FindAll(a => a.currentPiece.pieceType == robot.pieceType).Count();

                humanScore.text = "HUMAN SCORE : " + board.allUnitOfBoard.FindAll(x => x.currentPiece != null).FindAll(a => a.currentPiece.pieceType == human.pieceType).Count();

                if (IsGameOver(board.allUnitOfBoard))
                {
                    FinishGame();
                }
                else
                {
                    Turn(human);
                }
            }
        }

        public void TurnPiecesAfterPlayerPlayTurn(Unit playedUnit, BasePlayer currentPlayer, BasePlayer oppositePlayer)
        {

            List<Unit> allTurnaleUnitPieces = board.GetAllTurnableUnitPieces(playedUnit, board.allUnitOfBoard, currentPlayer.pieceType, oppositePlayer.pieceType);

            List<Piece> allOppositePieces = allTurnaleUnitPieces.Select(x => x.currentPiece).ToList();

            for (int i = 0; i < allOppositePieces.Count; i++)
            {
                PiecePoll.AddToPoll(allOppositePieces[i]);
            }

            allTurnaleUnitPieces.ForEach(x => x.SetUnitFree());

            allTurnaleUnitPieces.ForEach(x => board.SpawnPieceToBoard(x, PiecePoll.GetPiece(currentPlayer.pieceType)));
        }

        public bool IsGameOver(List<Unit> units)
        {
            return !units.Find(x => x.currentPiece == null);
        }

        public void FinishGame()
        {
            Debug.Log("GAME FINISHED");
        }

        public void SpawnInitialPiecesAtBoard(BasePlayer humanPlayer, BasePlayer robotPlayer, Board board)
        {
            for (int i = 0; i < 2; i++)
            {
                Piece humanPiece = PiecePoll.GetPiece(humanPlayer.pieceType);

                if (humanPiece.pieceType == PieceType.Black)
                {
                    Unit blackPieceInilitialPositionUnit = board.allUnitOfBoard.Find(x => x.unitBoardPosition == board.initialBlackPiecesPositions[i]);

                    board.SpawnPieceToBoard(blackPieceInilitialPositionUnit, humanPiece);
                }
                else if (humanPiece.pieceType == PieceType.White)
                {
                    Unit blackPieceInilitialPositionUnit = board.allUnitOfBoard.Find(x => x.unitBoardPosition == board.initialWhitePiecesPositions[i]);

                    board.SpawnPieceToBoard(blackPieceInilitialPositionUnit, humanPiece);
                }

                Piece robotPiece = PiecePoll.GetPiece(robotPlayer.pieceType);

                if (robotPiece.pieceType == PieceType.Black)
                {
                    Unit blackPieceInilitialPositionUnit = board.allUnitOfBoard.Find(x => x.unitBoardPosition == board.initialBlackPiecesPositions[i]);

                    board.SpawnPieceToBoard(blackPieceInilitialPositionUnit, robotPiece);

                }
                else if (robotPiece.pieceType == PieceType.White)
                {
                    Unit blackPieceInilitialPositionUnit = board.allUnitOfBoard.Find(x => x.unitBoardPosition == board.initialWhitePiecesPositions[i]);

                    board.SpawnPieceToBoard(blackPieceInilitialPositionUnit, robotPiece);
                }
            }
        }
    }
}
