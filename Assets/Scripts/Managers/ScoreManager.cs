using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    //public static ScoreManager Instance;

    [SerializeField] public TextMeshProUGUI scoreText;

    private string[] scoreValues = {"0","15","30","40","Adv"};
    
    private AudioSource[] audioSourcesWinLose;

    // void Awake()
    // {
    //     // Instance = this;
    //     GameManager.OnGameStateChanged += GameManagerOnBeginGame;
    // }
    //
    // void OnDestroy()
    // {
    //     GameManager.OnGameStateChanged -= GameManagerOnBeginGame;
    // }
    //
    // private void GameManagerOnBeginGame(GameState state) {
    //
    //     // call ServeBall on attached ball gameObject
    //
    //     // if win condition => set state = GameState.Victory
    //     // if lose condition => set state = GameState.Lose
    // }
    
    // Start is called before the first frame update
    void Start()
    {
        audioSourcesWinLose = GetComponents<AudioSource>();
        Debug.Log("Audios#="+audioSourcesWinLose.Length);
    }

    public void ResetScore()
    {
        scoreText.text = "0:0";
        scoreText.color = Color.white;

    }

    public void UpdateScore(bool leftPoint)
    {
        if (leftPoint)
            audioSourcesWinLose[2].Play();
        else
            audioSourcesWinLose[3].Play();
        
        scoreText.text = scoreValues[GameManager.Instance.playerScore] + ":" +  scoreValues[GameManager.Instance.enemyScore];
    }

    public void showWin()
    {
        audioSourcesWinLose[0].Play();
        scoreText.color = Color.green;
        scoreText.text = "You Win!";
    }

    public void showLose()
    {
        audioSourcesWinLose[1].Play();
        scoreText.color = Color.red;
        scoreText.text = "You Lose!";
    }

    //
    //
    // // Update is called once per frame
    // void Update()
    // {
    //     if (GameManager.Instance.playerScore > 4) {
    //         GameManager.Instance.UpdateGameState(GameState.Victory);
    //     }
    //     if (GameManager.Instance.enemyScore > 4) {
    //         GameManager.Instance.UpdateGameState(GameState.Lose);
    //     }
    //     //Debug.Log(GameManager.Instance.playerScore);
    // }
}
