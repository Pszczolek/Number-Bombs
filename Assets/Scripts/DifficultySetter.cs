using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySetter : MonoBehaviour
{
    public DifficultyOptions difficultyOptions;
    public int startingIndex = 0;

    [SerializeField]
    private QuantumTek.QuantumUI.QUI_OptionList optionList;
    [SerializeField]
    private Toggle[] operationToggles;
    [SerializeField]
    private Slider bombsPerMinuteSlider;
    [SerializeField]
    private TMP_InputField bombsPerMinuteInputText;
    [SerializeField]
    private Slider maxNumberSlider;
    [SerializeField]
    private TMP_InputField maxNumberInputText;

    private bool allowChange = true;


    private void Start()
    {
        startingIndex = difficultyOptions.selectedIndex;
        List<string> options = difficultyOptions.difficulties.Select(d => d.difficultyName).ToList();
        optionList.options = options;
        optionList.optionIndex = startingIndex;
        optionList.SetOption(startingIndex);
        difficultyOptions.ChangeDifficulty(startingIndex);
        DisplayDifficultyInfo();
    }
    public void SetDifficulty(int direction)
    {
        difficultyOptions.selectedIndex += direction;
        if (difficultyOptions.selectedIndex < 0)
            difficultyOptions.selectedIndex = difficultyOptions.difficulties.Length - 1;
        else if (difficultyOptions.selectedIndex >= difficultyOptions.difficulties.Length)
            difficultyOptions.selectedIndex = 0;

        DisplayDifficultyInfo();
    }

    public void ChangeDifficultyOptions()
    {
        if (allowChange == false)
            return;
        Debug.Log("Change difficulty settings");
        difficultyOptions.SelectedDifficulty.allowedOperations = OperationType.None;
        if(operationToggles[0].isOn == true)
        {
            Debug.Log("Allowing Summation");
            difficultyOptions.SelectedDifficulty.AllowOperation(OperationType.Summation);
        }
        if (operationToggles[1].isOn == true)
        {
            Debug.Log("Allowing Substraction");
            difficultyOptions.SelectedDifficulty.AllowOperation(OperationType.Substraction);
        }
        if (operationToggles[2].isOn == true)
        {
            Debug.Log("Allowing Multiplication");
            difficultyOptions.SelectedDifficulty.AllowOperation(OperationType.Multiplication);
        }
        if (operationToggles[3].isOn == true)
        {
            Debug.Log("Allowing Division");
            difficultyOptions.SelectedDifficulty.AllowOperation(OperationType.Division);
        }
        difficultyOptions.SelectedDifficulty.bombsPerMinute = bombsPerMinuteSlider.value;
        bombsPerMinuteInputText.text = bombsPerMinuteSlider.value.ToString();
        difficultyOptions.SelectedDifficulty.maxNumber = Mathf.FloorToInt(maxNumberSlider.value);
        maxNumberInputText.text = maxNumberSlider.value.ToString();
        difficultyOptions.SelectedDifficulty.AutoIncrement();
    }

    public void DisplayDifficultyInfo() {
        allowChange = false;
        operationToggles[0].isOn = difficultyOptions.SelectedDifficulty.IsOperationAllowed(OperationType.Summation);
        operationToggles[1].isOn = difficultyOptions.SelectedDifficulty.IsOperationAllowed(OperationType.Substraction);
        operationToggles[2].isOn = difficultyOptions.SelectedDifficulty.IsOperationAllowed(OperationType.Multiplication);
        operationToggles[3].isOn = difficultyOptions.SelectedDifficulty.IsOperationAllowed(OperationType.Division);
        bombsPerMinuteSlider.value = difficultyOptions.SelectedDifficulty.bombsPerMinute;
        bombsPerMinuteInputText.text = bombsPerMinuteSlider.value.ToString();
        maxNumberSlider.value = difficultyOptions.SelectedDifficulty.maxNumber;
        maxNumberInputText.text = maxNumberSlider.value.ToString();
        //for (int i = 0; i < operationToggles.Length; i++)
        //{
        //    operationToggles[i].isOn = difficultyOptions.SelectedDifficulty.IsOperationAllowed(i+1);
        //}
        EnableToggles();
        allowChange = true;
    }

    public void UpdateBombsPerMinuteSlider()
    {
        if (allowChange == false)
            return;
        allowChange = false;
        if(int.TryParse(bombsPerMinuteInputText.text, out int value))
        {
            bombsPerMinuteSlider.value = value;
            bombsPerMinuteInputText.text = bombsPerMinuteSlider.value.ToString();
        }
        allowChange = true;
        bombsPerMinuteSlider.onValueChanged?.Invoke(bombsPerMinuteSlider.value);
    }

    public void UpdateMaxNumberSlider()
    {
        if (allowChange == false)
            return;
        allowChange = false;
        if (int.TryParse(maxNumberInputText.text, out int value))
        {
            maxNumberSlider.value = value;
            maxNumberInputText.text = maxNumberSlider.value.ToString();
        }
        allowChange = true;
        maxNumberSlider.onValueChanged?.Invoke(maxNumberSlider.value);
    }

    private void EnableToggles()
    {
        bool isCustomDiff = difficultyOptions.SelectedDifficulty.customDifficulty;
        foreach (var t in operationToggles)
        {
            t.interactable = isCustomDiff;
        }
        bombsPerMinuteSlider.interactable = isCustomDiff;
        maxNumberSlider.interactable = isCustomDiff;
        bombsPerMinuteInputText.interactable = isCustomDiff;
        maxNumberInputText.interactable = isCustomDiff;
    }
}
