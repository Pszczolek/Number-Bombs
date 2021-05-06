using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public static InputReader Instance;
    public int currentNumber;

    public Action<int> OnDigitEntered;
    public Action<int> OnNumberFired;
    public Action OnEscape;

    private void Awake()
    {
        Instance = this;
    }

    void OnGUI()
    {
        int number = 0;
        Event e = Event.current;
        if (e.isKey)
        {
            Debug.Log("Detected key code: " + e.keyCode);
        }
        if (e.type != EventType.KeyDown || e.keyCode == KeyCode.None)
            return;
        
        KeyCode key = e.keyCode;
        if (key >= KeyCode.Alpha0 && key <= KeyCode.Alpha9)
        {
            number = (int)key - 48;
        }
        else if (key >= KeyCode.Keypad0 && key <= KeyCode.Keypad9)
        {
            number = (int)key - 256;
        }
        else if (key == KeyCode.Return || key == KeyCode.KeypadEnter)
        {
            NumberFired();
            return;
        }
        else if (key == KeyCode.Backspace)
        {
            ClearNumber();
            return;
        }
        else if (key == KeyCode.Escape)
        {
            EscapeClicked();
            return;
        }
        else
        {
            return;
        }
        //Debug.Log(number);
        DigitEntered(number);

    }

    private void Update()
    {

    }

    public void DigitEntered(int digit)
    {
        Debug.Log($"DigitEntered: {digit}");
        if (digit == 0 && currentNumber == 0)
            return;
        if (currentNumber >= 9999999)
            return;
        currentNumber = currentNumber*10 + digit;
        Debug.Log($"Current number: {currentNumber}");
        OnDigitEntered(currentNumber);
        ParticleTextController.Instance.TextEntered(currentNumber.ToString());
    }

    public void ClearNumber()
    {
        Debug.Log("ClearNumber");
        currentNumber = 0;
        OnDigitEntered(currentNumber);
    }

    public void NumberFired()
    {
        if (currentNumber == 0)
            return;
        Debug.Log("Number fired");
        OnNumberFired(currentNumber);
        ClearNumber();
    }

    public void EscapeClicked()
    {
        if (OnEscape != null)
            OnEscape();
    }
}
