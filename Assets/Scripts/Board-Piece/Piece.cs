using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Othello
{
    public class Piece : MonoBehaviour
    {
        public PieceType pieceType;

        public void SetPiecePositionAsVisual(Vector3 position)
        {
            this.transform.position = position + new Vector3(0, 0, -1);
        }

        public void SetPieceType(PieceType pieceType)
        {
            this.pieceType = pieceType;
        }
    }
}
