using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
	[SerializeField] public GameObject pauseMenu; //pause menu
	bool paused = false; //if paused

	// Update is called once per frame
	void Update()
	{
		//handles pause
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!paused)
			{
				PauseGame();
			}
			else
			{
				ResumeGame();
			}
		}
	}

	/// <summary>
	/// this pauses the game
	/// </summary> <summary>
	/// 
	/// </summary>
	void PauseGame()
	{
		pauseMenu.SetActive(true);
		Time.timeScale = 0f;
		paused = true;
		GameStateMachine.gameState = GameState.PAUSED;
		//GameManager.Instance.setGameState(GameState.Paused);
	}


	/// <summary>
	/// resumes the game
	/// </summary>
	void ResumeGame()
	{
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
		paused = false;
		GameStateMachine.gameState = GameState.GAME_PLAY;
		//GameManager.Instance.setGameState(GameState.Gameplay);
	}
}
