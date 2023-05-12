using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private string[] scoreValues = {"0","","15", "","30", "","40", "","Adv"};
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnBeginGame;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnBeginGame;
    }
    
    private void GameManagerOnBeginGame(GameState state) {
        scoreText.text = "0:0";

        // call ServeBall on attached ball gameObject

        // if win condition => set state = GameState.Victory
        // if lose condition => set state = GameState.Lose
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.playerScore > 4) {
            GameManager.Instance.UpdateGameState(GameState.Victory);
        }
        if (GameManager.Instance.enemyScore > 4) {
            GameManager.Instance.UpdateGameState(GameState.Lose);
        }
        //Debug.Log(GameManager.Instance.playerScore);
        scoreText.text = scoreValues[GameManager.Instance.playerScore] + ":" +  scoreValues[GameManager.Instance.enemyScore];
    }
}
