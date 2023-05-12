using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public ScoreManager scoreUI;
    
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    public int playerScore = 0, enemyScore = 0;

    void Awake()
    {
        Instance = this;
        // Starting at normal game
        UpdateGameState(GameState.MainMenu);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (State) {
            case GameState.MainMenu:
                HandleMainMenu();
                break;
            case GameState.TennisGame:
                HandlePlayerTurn();
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
    
    private void HandleMainMenu()
    {
        
    }

    private void HandlePlayerTurn()
    {
        scoreUI.ResetScore();
        // Should set up necessary things so that player begins
        // Will include enabling movement scripts
        
    }

    private void HandleVictory()
    {
        // FOR NOW 
        Instance.UpdateGameState(GameState.MainMenu);
    }
    private void HandleLose()
    {
        Instance.UpdateGameState(GameState.MainMenu);
    }

    public void PointScoredByPlayer(bool b)
    {
        if (b) // Player Scored
        {
            if (playerScore == 6 || playerScore == 7) // Player had 40 pts
            {
                if (enemyScore == 6)
                {
                    // Player-Enemy were at DEUCE
                    // Player moves to Adv
                    playerScore += 1;
                }
                else if (enemyScore > 6) // enemyScore was at Adv
                {
                    enemyScore -= 1; // No point change in Player, Enemy point changed back to 40
                }
                else
                {
                    // If Player was at 40, then scoring this point means they win
                    Instance.UpdateGameState(GameState.Victory);
                    return;
                }
            }
            else if (playerScore == 8) // Player was at Adv
            {
                // Player wins
                Instance.UpdateGameState(GameState.Victory);
                // TODO: CHECK will this break?
            }
            else // Normal scenario, point increase (SHOULD WORK WITH DOUBLE TRIGGER ISSUE
            {
                playerScore += 1;
            }
        }
        else
        {
            if (enemyScore == 6 || enemyScore == 7) // Player had 40 pts
            {
                if (playerScore == 6)
                {
                    // Player-Enemy were at DEUCE
                    // Player moves to Adv
                    enemyScore += 1;
                }
                else if (playerScore > 6) // enemyScore was at Adv
                {
                    playerScore -= 1; // No point change in Player, Enemy point changed back to 40
                }
                else
                {
                    // If Player was at 40, then scoring this point means they win
                    Instance.UpdateGameState(GameState.Lose);
                    return;
                }
            }
            else if (enemyScore== 8) // Player was at Adv
            {
                // Player wins
                Instance.UpdateGameState(GameState.Lose);
                // will this break?
            }
            else // Normal scenario, point increase (SHOULD WORK WITH DOUBLE TRIGGER ISSUE
            {
                enemyScore += 1;
            }

        }
        
        // Update Score
        scoreUI.UpdateScore();
    }



}

public enum GameState {
    MainMenu,
    TennisGame,
    Victory,
    Lose
}
