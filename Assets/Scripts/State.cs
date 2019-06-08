using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public static State Instance = new State();

    private State() { }

    public GameState Game_State { get; set; }

    public enum GameState
    {
        Starting,
        AtHuman,
        AtRobot,
        GameOver
    }

    public void SetState(GameState state)
    {
        Game_State = state;
    }
}
