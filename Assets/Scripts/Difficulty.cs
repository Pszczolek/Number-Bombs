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
}
