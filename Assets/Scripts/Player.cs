using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public int baseLifes = 3;
    public int currentLifes = 3;
    public int score = 0;

    bool dead = false;

    public Action<int> OnHealthChanged;
    public Action<int> OnScoreChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Restart();
        GameManager.Instance.OnGameRestart += Restart;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameRestart -= Restart;
    }

    public void Hit()
    {
        if (dead == true)
            return;

        currentLifes--;
        if(OnHealthChanged != null)
            OnHealthChanged(currentLifes);
        if(currentLifes <= 0)
        {
            dead = true;
            GameManager.Instance.GameOver();
        }
    }

    public void Restart()
    {
        dead = false;
        currentLifes = baseLifes;
        score = 0;
        if (OnHealthChanged != null)
            OnHealthChanged(currentLifes);
        if (OnScoreChanged != null)
        {
            OnScoreChanged(score);
        }
    }

    public void AddScore(int value)
    {
        if (dead == true)
            return;

        score += value;
        if(OnScoreChanged != null)
        {
            OnScoreChanged(score);
        }
    }
}
