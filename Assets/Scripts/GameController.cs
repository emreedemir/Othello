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

        }

        public void Turn(BasePlayer player)
        {
            player.Play(board);


        }
    }
}
