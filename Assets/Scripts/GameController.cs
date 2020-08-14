using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Othello
{
    public class GameController : MonoBehaviour
    {
        public Human human;

        public Robot robot;

        public PiecePoll piecePool;

        public Board board;

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

            SpawnInitialPiecesAtBoard(human, robot, board);

            List<Unit> playableUnitsForHuman = Board.GetPlayableUnits(human.pieceType, robot.pieceType, board.allUnitOfBoard);

            if (playableUnitsForHuman.Count > 0)
            {

                for (int i = 0; i < playableUnitsForHuman.Count; i++)
                {
                    playableUnitsForHuman[i].GetComponent<SpriteRenderer>().color = Color.red;
                }
            }

        }

        public void Turn(BasePlayer player)
        {
            player.Play(board);
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
