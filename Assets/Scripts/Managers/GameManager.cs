using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;

    private Game game;

    public ScoreManager scoreUI;
    
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    public int playerScore = 0;
    public int enemyScore = 0;

    void Awake()
    {
        Instance = this;
        // Starting at normal game
        game = ball.GetComponent<Game>();
       
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
                ball.GetComponent<BallState>().setState(3);
                HandleVictory();
                break;
            case GameState.Lose:
                ball.GetComponent<BallState>().setState(3);
                HandleLose();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);

    }
    
    private void HandleMainMenu()
    {
        playerScore = 0;
        enemyScore = 0;
        ball.GetComponent<BallState>().setState(3);
    }

    private void HandlePlayerTurn()
    {
        scoreUI.ResetScore();
        // Should set up necessary things so that player begins
        // Will include enabling movement scripts
        StartCoroutine(game.ServeBall(false));
    }

    private async void HandleVictory()
    {
        scoreUI.showWin();
        // FOR NOW 
        await System.Threading.Tasks.Task.Delay(5000);
        Instance.UpdateGameState(GameState.MainMenu);
    }
    private async void HandleLose()
    {
        scoreUI.showLose();
        await System.Threading.Tasks.Task.Delay(5000);
        Instance.UpdateGameState(GameState.MainMenu);
    }

    public void PointScoredByPlayer(bool b)
    {
        if (b) // Player Scored
        {
            if (playerScore == 3) // Player had 40 pts
            {
                if (enemyScore == 3)
                {
                    // Player-Enemy were at DEUCE
                    // Player moves to Adv
                    playerScore += 1;
                }
                else if (enemyScore == 4) // enemyScore was at Adv
                {
                    enemyScore -= 1; // No point change in Player, Enemy point changed back to 40
                }
                else
                {
                    // If Player was at 40, then scoring this point means they win
                    Debug.Log("YOU WIN!");
                    Instance.UpdateGameState(GameState.Victory);
                    return;
                }
            }
            else if (playerScore == 4) // Player was at Adv
            {
                // Player wins
                Debug.Log("YOU WIN!");
                Instance.UpdateGameState(GameState.Victory);
                // TODO: CHECK will this break?
                return;
            }
            else // Normal scenario, point increase (SHOULD WORK WITH DOUBLE TRIGGER ISSUE
            {
                playerScore += 1;
            }
        }
        else
        {
            if (enemyScore == 3) // Player had 40 pts
            {
                if (playerScore == 3)
                {
                    // Player-Enemy were at DEUCE
                    // Player moves to Adv
                    enemyScore += 1;
                }
                else if (playerScore == 4) // enemyScore was at Adv
                {
                    playerScore -= 1; // No point change in Player, Enemy point changed back to 40
                }
                else
                {
                    // If Player was at 40, then scoring this point means they win
                    Debug.Log("YOU LOSE!");
                    Instance.UpdateGameState(GameState.Lose);
                    return;
                }
            }
            else if (enemyScore== 4) // Player was at Adv
            {
                // Player wins
                Debug.Log("YOU LOSE!");
                Instance.UpdateGameState(GameState.Lose);
                return;
                // will this break?
            }
            else // Normal scenario, point increase (SHOULD WORK WITH DOUBLE TRIGGER ISSUE
            {
                enemyScore += 1;
            }

        }
        
        Debug.Log("PlayerScore="+playerScore + ", EnemyScore="+enemyScore);
        
        // Update Score
        scoreUI.UpdateScore(b);
    }
}

public enum GameState {
    MainMenu,
    TennisGame,
    Victory,
    Lose
}
