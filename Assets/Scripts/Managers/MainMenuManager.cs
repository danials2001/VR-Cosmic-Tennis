using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    void Awake()
    {
        GameManager.OnGameStateChanged += EnableMainMenu;
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
                break;
            case 1:
                // Right-Handed paddle
                // TODO: Switch handedness
                Debug.Log("Right-Hand mode selected");
                break;
        }
    }

    // Dropdown switch
    

}
