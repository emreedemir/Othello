using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay  :MonoBehaviour{

    //This class will contain Game's Rules;

    Player m_player1;
    Player m_player2;
    AIPlayer m_aiplayer;


    PiecePool m_piecePool;


    #region Onset Positions
    //For Player 1
    int x1_P1 = 4;
    int y1_P1 = 4;

    int x2_P1 = 5;
    int y2_P1 = 5;

    //For Player1 or AI
    int x1_P2 = 4;
    int y1_P2 = 5;


    int x2_P2 = 5;
    int y2_P2 = 4;

    #endregion

    public void GetPlayers(Player player1,Player player2)
    {
        m_player1 = player1;
        m_player2 = player2;
        Onset(m_player1,m_player2);
    }

    public void GetPlayers(Player player1, AIPlayer aiPlayer)
    {
        m_player1 = player1;
        m_aiplayer = aiPlayer;
        Onset(m_player1,m_aiplayer);
    }

    //We use BasePlayer parameters for that functions because player2 can be ai or player2.
    public void Onset(BasePlayer player1, BasePlayer player2)
    {
        player1.AddPiece(m_piecePool.TakeFromPool(Piece.PieceType.WhitePiece), new Position(x1_P1, y1_P1));
        player2.AddPiece(m_piecePool.TakeFromPool(Piece.PieceType.BlackPiece), new Position(x1_P2, y1_P2));
    }

    public void Move()
    {

    }

}
