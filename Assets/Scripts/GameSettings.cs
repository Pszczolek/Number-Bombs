using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Settings", menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    [Range(0, 1)]
    public float volume = 1;

    public Action<float> OnVolumeChanged;

    public void SetVolume(float value)
    {
        volume = value;
        if(OnVolumeChanged != null)
        {
            OnVolumeChanged(volume);
        }
    }

}
