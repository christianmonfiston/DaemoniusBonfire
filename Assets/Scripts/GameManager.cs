using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameManager() {}
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
                instance = new GameManager();

            return instance;
        }
    }
    
    public delegate void GameStateHandler(GameState newGameState);
    public event GameStateHandler OnGameStateChanged;
    public GameState CurrentGameState { get; private set; }
    public void setGameState(GameState newGameState)
    {
        if(CurrentGameState != newGameState)
        {
            CurrentGameState = newGameState;
            OnGameStateChanged?.Invoke(newGameState);
        }
    }

   
}