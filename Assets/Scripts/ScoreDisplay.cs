using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        Player.Instance.OnScoreChanged += UpdateScore;
    }

    private void OnDestroy()
    {
        Player.Instance.OnScoreChanged -= UpdateScore;
    }

    public void UpdateScore(int value)
    {
        _text.text = value.ToString();
    }
}
