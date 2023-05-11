using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    public int playerScore = 0, enemyScore = 0;

    void Awake()
    {
        Instance = this;
        // Starting at normal game
        UpdateGameState(GameState.TennisGame);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (State) {
            case GameState.MainMenu:
                HandleMainMenu();
                break;
            case GameState.TennisGame:
                break;
            case GameState.Victory:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);

    }
    
    private void HandleMainMenu() {
        
    }


}

public enum GameState {
    MainMenu,
    TennisGame,
    Victory,
    Lose
}
