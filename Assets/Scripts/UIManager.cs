﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    int pauseMenuIndex;

    private bool gamePaused;

    private void Awake()
    {
        if(FindObjectsOfType<UIManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        InputReader.Instance.OnEscape += ToggleMenu;
        GameManager.Instance.OnGameOver += LoadMissedEquations;
    }

    private void OnDestroy()
    {
        InputReader.Instance.OnEscape -= ToggleMenu;
        GameManager.Instance.OnGameOver -= LoadMissedEquations;
    }
    public void ToggleMenu()
    {
        Debug.Log("Toggle Menu");
        gamePaused = !gamePaused;
        GameManager.Instance.PauseGame();
        if (gamePaused)
        {
            SceneManager.LoadSceneAsync(pauseMenuIndex, LoadSceneMode.Additive);

        }
        else
        {
            SceneManager.UnloadSceneAsync(pauseMenuIndex);
        }

    }

    public void LoadMissedEquations()
    {
        SceneManager.LoadSceneAsync("MissedBombs", LoadSceneMode.Additive);
    }

    public void CloseMissedEquations()
    {
        SceneManager.UnloadSceneAsync("MissedBombs");
    }

    public void LoadHighScores()
    {
        SceneManager.LoadSceneAsync("HighScores", LoadSceneMode.Additive);
    }

    public void CloseHighScores()
    {
        SceneManager.UnloadSceneAsync("HighScores");
    }


}
