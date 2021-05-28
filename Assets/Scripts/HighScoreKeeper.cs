using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public struct HighScore
{
    public string name;
    public int score;
}

[System.Serializable]
public class HighScores
{
    public string difficultyName;
    public int maxScoreCount = 5;
    public List<HighScore> scores = new List<HighScore>();

    public HighScores(string difficultyName)
    {
        this.difficultyName = difficultyName;
        for(int i = 0; i < maxScoreCount; i++)
        {
            scores.Add(new HighScore { name = "", score = 0 });
        }
    }

    public bool CheckForHighScore(int value)
    {
        if (maxScoreCount > scores.Count)
            return true;
        return scores.Any(x => x.score < value);
    }

    public void AddHighScore(int value, string name)
    {
        AddHighScore(new HighScore { name = name, score = value });
    }

    public void AddHighScore(HighScore value)
    {
        scores.Add(value);
        scores = scores.OrderByDescending(x => x.score).ToList();
        if(scores.Count > maxScoreCount)
        {
            scores.RemoveAt(scores.Count - 1);
        }
    }
}

[System.Serializable]
public class HighScoresList
{
    public List<HighScores> list = new List<HighScores>();

    public HighScoresList(List<string> difficultyNames)
    {
        foreach(var name in difficultyNames)
        {
            list.Add(new HighScores(name));
        }
    }

    public HighScores GetByDifficulty(string difficultyName)
    {
        Debug.Log("GetByDifficulty");
        var output = list.FirstOrDefault(x => x.difficultyName == difficultyName);
        if (output == null)
        {
            Debug.Log("Difficulty null");
            HighScores newHighScores = new HighScores(difficultyName);
            list.Add(newHighScores);
            output = newHighScores;
        }
        return output;
    }

    public List<string> GetDifficultyNames()
    {
        var difficultiesList = list.Select(s => s.difficultyName).ToList();
        return difficultiesList;
    }
}

public class HighScoreKeeper : MonoBehaviour
{
    public const string HIGH_SCORES_KEY = "HighScores";
    public HighScoresList highScores;
    //public Dictionary<string, HighScores> HighScoresByDifficulty;
    public HighScore testScore;
    public string diffName;
    [SerializeField] DifficultyOptions difficultyOptions;

    private void Awake()
    {
        LoadScores();
        if(highScores == null)
        {
            var difficultyNames = difficultyOptions.difficulties.Where(d => d.customDifficulty == false).Select(d => d.difficultyName).ToList();
            highScores = new HighScoresList(difficultyNames);
        }
        foreach(var difficultyName in difficultyOptions.GetDifficultyNames())
        {
            Debug.Log("Difficulty:" + difficultyName);
            highScores.GetByDifficulty(difficultyName);
        }

    }

    [ContextMenu("AddScore")]
    public void TestAdd()
    {
        AddHighScore(testScore, diffName);
    }

    [ContextMenu("GetScore")]
    public void TestGet()
    {
        var test = GetHighScores(diffName);
        foreach (var item in test.scores)
        {
            Debug.Log($"{item.name} : {item.score}");
        }

    }

    public void AddHighScore(HighScore highScore, string difficultyName)
    {
        var highscoresByDifficulty = highScores.GetByDifficulty(difficultyName);
        highscoresByDifficulty.AddHighScore(highScore);
        SaveScores();
    }

    public HighScores GetHighScores(string difficultyName)
    {
        return highScores.GetByDifficulty(difficultyName);
    }

    public bool CheckForHighScore(int value, string difficultyName)
    {
        return highScores.GetByDifficulty(difficultyName).CheckForHighScore(value);
    }

    [ContextMenu("Save")]
    public void SaveScores()
    {
        string scoresString = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString(HIGH_SCORES_KEY, scoresString);
    }

    public void LoadScores()
    {
        string scoresString = PlayerPrefs.GetString(HIGH_SCORES_KEY);
        highScores = JsonUtility.FromJson<HighScoresList>(scoresString);
        Debug.Log(scoresString);
        //HighScoresByDifficulty = highScores.list.ToDictionary(x => x.difficultyName);

    }
}
