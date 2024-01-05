using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu;
    bool paused = false;  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!paused)
            {
                PauseGame();
            }
            else 
            {
                ResumeGame();
            }
        }
    }

    void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
        //GameManager.Instance.setGameState(GameState.Paused);
    }

    void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
        //GameManager.Instance.setGameState(GameState.Gameplay);
    }
}
