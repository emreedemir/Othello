using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

    
    #region UI Buttons
    public Button onePlayerButton;
    public Button twoPlayerButton;
    public Button resetButton;
    public Button exitButton;

    #endregion


    #region Player Types
    public Player m_player;
    public AIPlayer m_aiPLayer;

    #endregion

    public enum GameType
    {
        DefaultType =0,
        OnePlayer = 1,
        TwoPlayer = 2
    }



    public void Start()
    {
        onePlayerButton.onClick.AddListener(()=> { SetGame(GameType.OnePlayer); });
        twoPlayerButton.onClick.AddListener(() => { SetGame(GameType.TwoPlayer); });
        resetButton.onClick.AddListener(()=> { ResetGame(); });
        exitButton.onClick.AddListener(()=> { ExitGame(); });
    }

    public void SetGame(GameType gameType)
    {

        Debug.Log("Setting Game"+gameType);

        CreatePlayer(gameType);


    }

    public void ResetGame()
    {
        Debug.Log("Reset Game");
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
    }


    public void CreatePlayer(GameType gameType)
    {
        
        if (gameType == GameType.OnePlayer)
        {
            Player player1 = Instantiate(m_player);
            
            player1.transform.name = "PLAYER";

            AIPlayer aiPlayer = Instantiate(m_aiPLayer);
            aiPlayer.transform.name = "AI PLAYER";

        }
        else if (gameType == GameType.TwoPlayer)
        {
            Debug.Log("is erro");
            Player player1 = Instantiate(m_player);
            player1.transform.name = "PLAYER 1";

            Player player2 = Instantiate(m_player);
            player1.transform.name = "PLAYER 2";

        }
        else
        {
            Debug.LogWarning("Error at GameType");
        }

    }


}
