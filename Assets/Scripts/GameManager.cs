using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public Action OnGameOver;
    public Action OnGameRestart;

    public bool isGamePaused;
    [SerializeField] GameSession gameSession;
    [Range(0, 1)]
    [SerializeField] float timeScale;

    private void Awake()
    {
        Instance = this;
        gameSession.isGameStarted = true;
    }

    private void Update()
    {
        Time.timeScale = timeScale;
    }

    public void GameOver()
    {
        if(OnGameOver != null)
        {
            OnGameOver();
        }
    }

    public void GameRestart()
    {
        if (OnGameRestart != null)
        {
            OnGameRestart();
        }
    }

    public void PauseGame()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Debug.Log("Game Paused");
            Time.timeScale = 0;
        }

        else
        {
            Time.timeScale = 1;
            Debug.Log("Game UnPaused");
        }

    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
        gameSession.isGameStarted = false;
    }

    //public void UnPauseGame()
    //{
    //    //
    //    Debug.Log("Game UnPaused");
    //}

}
