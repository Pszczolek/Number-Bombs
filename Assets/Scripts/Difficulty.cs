using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Difficulty
{
    public string difficultyName = "Very Easy";
    public OperationType allowedOperations = OperationType.Summation | OperationType.Substraction;
    public float bombsPerMinute = 10;
    public float bombSpeed = 2;
    public int minNumber = 1;
    public int maxNumber = 20;
    public float bombsPerMinuteIncrement = 1;
    public float bombSpeedIncrement = 0;
    public float timeToIncrement = 60;
    public bool customDifficulty = false;

    public bool IsOperationAllowed(int index)
    {
        return (allowedOperations & (OperationType)(1 << (index - 1))) ==
                        (OperationType)(1 << (index - 1)) ? true : false;
    }

    public bool IsOperationAllowed(OperationType operation)
    {
        return allowedOperations.HasFlag(operation);
    }

    public void AllowOperation(OperationType operation)
    {
        Debug.Log($"Allowed before change: {allowedOperations}");
        allowedOperations |= operation;
        Debug.Log($"Allowed after change: {allowedOperations}");
    }

    public void AutoIncrement()
    {
        float increment = Mathf.Floor(bombsPerMinute/10);
        if(increment > 0)
        {
            timeToIncrement = Mathf.Ceil(60 / increment);
        }
        else
        {
            timeToIncrement = 60;
        }
        bombsPerMinuteIncrement = 1;
    }

}
