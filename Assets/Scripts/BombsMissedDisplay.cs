using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BombsMissedDisplay : MonoBehaviour
{

    [SerializeField] GameSession gameSession;
    [SerializeField] GameObject equationsGrid;
    [SerializeField] GameObject equationPrefab;
    [SerializeField] GameObject bombsGrid;
    [SerializeField] Image bombImage;
    // Start is called before the first frame update
    void Start()
    {
        DisplayMissedEquations();
    }

    public void DisplayMissedEquations()
    {
        for(int i = 0; i < equationsGrid.transform.childCount; i++)
        {
            Destroy(equationsGrid.transform.GetChild(i));
        }

        for(int i = 0; i < bombsGrid.transform.childCount; i++)
        {
            Destroy(bombsGrid.transform.GetChild(i));
        }

        foreach (string eq in gameSession.missedEquations)
        {
            GameObject newEquation = Instantiate(equationPrefab, equationsGrid.transform);
            newEquation.GetComponent<TMP_Text>().text = eq;
            Instantiate(bombImage, bombsGrid.transform);
        }
    }

    public void Close()
    {
        UIManager.Instance.LoadHighScores();
        UIManager.Instance.CloseMissedEquations();
    }

}
