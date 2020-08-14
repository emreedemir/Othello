using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Othello
{
    public class PiecePoll
    {
        public Piece piecePrefab;

        public static Queue<Piece> whitePieces;

        public static Queue<Piece> blackPieces;

        public const int POLL_SIZE = 64;

        public Transform piecesParent;

        public void CreatePoll()
        {
            piecePrefab = Resources.Load<Piece>("Prefab/piecePrefab");

            GameObject piecesParentGameObject = new GameObject("Piece Poll");

            piecesParent = piecesParentGameObject.transform;

            whitePieces = new Queue<Piece>(POLL_SIZE);

            blackPieces = new Queue<Piece>(POLL_SIZE);

            for (int i = 0; i < POLL_SIZE; i++)
            {
                Piece newWhitePiece = MonoBehaviour.Instantiate(piecePrefab);

                newWhitePiece.GetComponent<SpriteRenderer>().color = Color.white;
                newWhitePiece.SetPieceType(PieceType.White);

                newWhitePiece.transform.SetParent(piecesParent);

                AddToPoll(newWhitePiece);

                Piece newBlackPiece = MonoBehaviour.Instantiate(piecePrefab);

                newBlackPiece.SetPieceType(PieceType.Black);

                newBlackPiece.GetComponent<SpriteRenderer>().color = Color.black;

                newBlackPiece.transform.SetParent(piecesParent);

                AddToPoll(newBlackPiece);
            }
        }

        public static Piece GetPiece(PieceType pieceType)
        {
            if (pieceType == PieceType.Black)
            {
                if (blackPieces.Count > 0)
                    return blackPieces.Dequeue();
                else
                    return null;
            }
            else if (pieceType == PieceType.White)
            {
                if (whitePieces.Count > 0)
                    return whitePieces.Dequeue();
                else
                    return null;
            }

            return null;
        }

        public static void AddToPoll(Piece piece)
        {
            piece.gameObject.SetActive(false);

            if (piece.pieceType == PieceType.Black)
            {
                blackPieces.Enqueue(piece);
            }
            else if (piece.pieceType == PieceType.White)
            {
                whitePieces.Enqueue(piece);
            }
        }
    }
}
