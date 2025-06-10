using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateModel
{
    public enum GameState
    {
        Title = 0,
        displayingText = 1,
        ReadyOnGame = 2,
        OnGame = 3,
        Result = 4,
    }
}
