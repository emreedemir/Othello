using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance = new GameController();

    public enum PieceType
    {
        None,
        White,
        Black
    }

    public Button newGameButton;
    public Button exitGameButton;

    public Human Human { get; set; }
    public Robot Robot { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Board.Instance.CreateBoard();
        RenderButtons();
        Human = FindObjectOfType<Human>();
        Robot = FindObjectOfType<Robot>();

    }

    public void RenderButtons()
    {
        newGameButton.onClick.AddListener(() => SetGame());
        exitGameButton.onClick.AddListener(Application.Quit);
    }

    void SetGame()
    {
        Human.InitiliazePlayer(PieceType.Black);
        Robot.InitiliazePlayer(PieceType.White);
        Board.Instance.Initialize(Human, Robot);
        State.Instance.SetState(State.GameState.AtHuman);
    }

    public void EvaluateMove(Unit unit)
    {
        if (State.Instance.Game_State == State.GameState.AtHuman)
        {
            if (Board.Instance.IsPlayable(unit,GameController.Instance.Human.playerPieceType))
            {
                Board.Instance.SetPieceToBoard(GameController.Instance.Human.GetPiece(), unit);
                EvaluateRound(GameController.Instance.Human);
            }      
        }
        else
        {
            Debug.Log("Wait,Robot is playing");
        }
    }

    public void EvaluateRound(BasePlayer player)
    {
        if (player is Human)
        {
            Board.Instance.SearchPieces();
            /*
            if (!IsGameOver())
            {
                State.Instance.SetState(State.GameState.AtRobot);
            }
            */
        }
        else if (player is Robot)
        {
            Board.Instance.SearchPieces();

            if (!IsGameOver())
            {
                State.Instance.SetState(State.GameState.AtHuman);
            }
        }           
    }

    bool IsGameOver()
    {
        return false;
    }
}
