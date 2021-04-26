using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DifficultySetter : MonoBehaviour
{
    public DifficultyOptions difficultyOptions;
    public int startingIndex = 0;

    [SerializeField]
    private QuantumTek.QuantumUI.QUI_OptionList optionList;


    private void Start()
    {
        startingIndex = difficultyOptions.selectedIndex;
        List<string> options = difficultyOptions.difficulties.Select(d => d.difficultyName).ToList();
        optionList.options = options;
        optionList.optionIndex = startingIndex;
        optionList.SetOption(startingIndex);
        difficultyOptions.ChangeDifficulty(startingIndex);
    }
    public void SetDifficulty(int direction)
    {
        difficultyOptions.selectedIndex += direction;
        if (difficultyOptions.selectedIndex < 0)
            difficultyOptions.selectedIndex = difficultyOptions.difficulties.Length - 1;
        else if (difficultyOptions.selectedIndex >= difficultyOptions.difficulties.Length)
            difficultyOptions.selectedIndex = 0;
    }
}
