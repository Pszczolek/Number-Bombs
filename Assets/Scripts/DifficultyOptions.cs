using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Difficulty Options", menuName = "Difficulty Options")]
public class DifficultyOptions : ScriptableObject
{
    public Difficulty[] difficulties;
    public int selectedIndex = 0;
    public Difficulty SelectedDifficulty { 
        get {
            if (selectedIndex >= difficulties.Length)
                selectedIndex = 0;          
            return difficulties[selectedIndex];
        } }

    public void ChangeDifficulty(int index)
    {
        selectedIndex = index;
        if (selectedIndex >= difficulties.Length)
            selectedIndex = difficulties.Length - 1;
        else if (selectedIndex < 0)
            selectedIndex = 0;
    }
}
