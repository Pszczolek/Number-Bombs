using QuantumTek.QuantumUI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoresUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text namesDisplay;
    [SerializeField]
    TMP_Text scoresDisplay;
    [SerializeField]
    TMP_Text nameValue;
    [SerializeField]
    TMP_Text scoreValue;
    [SerializeField]
    TMP_Text difficultyValue;
    [SerializeField]
    TMP_Text yourDifficulty;
    [SerializeField]
    TMP_Text yourScore;
    [SerializeField]
    QUI_Window newHighScoreWindow;
    [SerializeField]
    QUI_Window yourScoreWindow;
    [SerializeField]
    QUI_OptionList optionsList;
    [SerializeField]
    HighScoreKeeper scoreKeeper;
    [SerializeField]
    GameSession gameSession;

    private void Awake()
    {
        optionsList.options.Clear();
        foreach (var itm in scoreKeeper.highScores.GetDifficultyNames())
        {
            optionsList.options.Add(itm);
        }
        optionsList.SetOption(0);



    }
    // Start is called before the first frame update
    void Start()
    {
        if (gameSession.isGameStarted)
        {
            if (IsHighScore())
            {
                newHighScoreWindow.SetActive(true);
                difficultyValue.text = gameSession.currentDifficulty;
                scoreValue.text = gameSession.score.ToString();
            }
            else
            {
                yourScoreWindow.SetActive(true);
                yourDifficulty.text = gameSession.currentDifficulty;
                yourScore.text = gameSession.score.ToString();
            }
        }

        UpdateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDisplay()
    {
        string selectedOption = optionsList.option;
        HighScores highScores = scoreKeeper.GetHighScores(selectedOption);
        List<string> namesToDisplay = highScores.scores.Select(x => x.name).ToList();
        List<string> scoresToDisplay = highScores.scores.Select(x => x.score.ToString()).ToList();
        StringBuilder builder = new StringBuilder();

        foreach(var name in namesToDisplay) {
            builder.AppendLine(name);
        }
        namesDisplay.text = builder.ToString();
        builder.Clear();
        foreach (var score in scoresToDisplay)
        {
            builder.AppendLine(score);
        }
        scoresDisplay.text = builder.ToString();

    }

    public void AddHighScore()
    {
        if (nameValue.text.Length < 2)
            return;
        Debug.Log("Length = " + nameValue.text.Length);
        HighScore scoreToAdd = new HighScore { name = nameValue.text, score = int.Parse(scoreValue.text) };
        scoreKeeper.AddHighScore(scoreToAdd, difficultyValue.text);
        newHighScoreWindow.SetActive(false);
        optionsList.SetOption(optionsList.options.IndexOf(difficultyValue.text));
    }

    public void Close()
    {
        UIManager.Instance.CloseHighScores();
    }

    private bool IsHighScore()
    {
        return scoreKeeper.CheckForHighScore(gameSession.score, gameSession.currentDifficulty);
    }
}
