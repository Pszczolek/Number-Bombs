using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private GameSettings gameSettings;
    private void Awake()
    {
        if (volumeSlider)
        {
            volumeSlider.value = gameSettings.volume;
        }
    }

    public void ClosePauseMenu()
    {
        UIManager.Instance.ToggleMenu();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
