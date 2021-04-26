using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnGameOver += FadeIn;
        GameManager.Instance.OnGameRestart += FadeOut;
    }



    private void OnDestroy()
    {
        GameManager.Instance.OnGameOver -= FadeIn;
        GameManager.Instance.OnGameRestart -= FadeOut;
    }
    public void FadeIn()
    {
        _animator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        _animator.SetTrigger("FadeOut");
    }

}
