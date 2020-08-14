using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Othello
{
    public class Unit : MonoBehaviour
    {

        /// <summary>
        /// Logical Position of Unit
        /// </summary>
        public Position unitBoardPosition;

        public Action<Unit> OnUnitPressed;

        public Piece currentPiece = null;

        public List<Unit> unitNeigbours;

        /// <summary>
        /// Set Unit position as logical and visual
        /// </summary>
        /// <param name="unitBoardPosition"></param>
        public void SetUnitPosition(Position unitBoardPosition)
        {
            this.unitBoardPosition = unitBoardPosition;

            transform.position = (Vector2)unitBoardPosition;
        }

        /// <summary>
        /// Set currentUnitPiece inside Unit
        /// </summary>
        /// <param name="piece"></param>
        public void SetUnitPiece(Piece piece)
        {
            this.currentPiece = piece;
        }


        /// <summary>
        /// Make currentPiece ==null inside Unit
        /// </summary>
        public void SetUnitFree()
        {
            currentPiece = null;
        }

        private void OnMouseDown()
        {
            OnUnitPressed?.Invoke(this);
        }


    }
}
