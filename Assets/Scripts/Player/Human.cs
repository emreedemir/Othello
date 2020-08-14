using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Othello
{
    public class Human : BasePlayer
    {

        public override void Play(Board board)
        {
            board.IsBoardPiecesTouchable = true;
        }
    }
}
