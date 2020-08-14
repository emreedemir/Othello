using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Othello
{
    public class Robot : BasePlayer
    {
        MinimaxAlgoritm minimaxAlgoritm;

        //////??????????????????

        public Robot()
        {
            minimaxAlgoritm = new MinimaxAlgoritm();
        }


        //////??????????????????
        public override void Play(Board board)
        {
            Unit bestUnitForPlay = minimaxAlgoritm.GetBestPiece(board.allUnitOfBoard, pieceType, oppositePieceType, 10);

        }


    }
}
