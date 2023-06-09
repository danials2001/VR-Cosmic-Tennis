using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject L_Paddle;
    [SerializeField] private GameObject R_Paddle;
    [SerializeField] private JetPack playerJetpack;
    [SerializeField] private GameObject L_Controller;
    [SerializeField] private GameObject R_Controller;
    
    void Awake()
    {
        GameManager.OnGameStateChanged += EnableMainMenu;
        dropdownHandedness(0);
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= EnableMainMenu;
    }
    
    private void EnableMainMenu(GameState state) {
        // sets MainMenuCanvas as active
        gameObject.SetActive(state == GameState.MainMenu);
    }
    
    // Start button OnClick() behavior
    public void clickStartButton()
    {
        // Starts Tennis Game
        GameManager.Instance.UpdateGameState(GameState.TennisGame);
    }

    public void dropdownHandedness(int option)
    {
        switch (option)
        {
            case 0:
                // Left-Handed paddle
                L_Paddle.SetActive(true);
                R_Paddle.SetActive(false);
                L_Controller.SetActive(false);
                R_Controller.SetActive(true);
                playerJetpack.setInputController(false);
                Debug.Log("Left-Hand Paddle mode selected");

                break;
            case 1:
                // Right-Handed paddle
                // TODO: Switch handedness
                L_Paddle.SetActive(false);
                R_Paddle.SetActive(true);
                L_Controller.SetActive(true);
                R_Controller.SetActive(false);
                playerJetpack.setInputController(true);
                Debug.Log("Right-Hand Paddle mode selected");
                break;
        }
    }

    // Dropdown switch
    

}
