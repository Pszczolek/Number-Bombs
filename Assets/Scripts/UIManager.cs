using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    int pauseMenuIndex;

    private bool gamePaused;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InputReader.Instance.OnEscape += ToggleMenu;
    }
    public void ToggleMenu()
    {
        Debug.Log("Toggle Menu");
        gamePaused = !gamePaused;
        GameManager.Instance.PauseGame();
        if (gamePaused)
        {
            SceneManager.LoadSceneAsync(pauseMenuIndex, LoadSceneMode.Additive);

        }
        else
        {
            SceneManager.UnloadSceneAsync(pauseMenuIndex);
        }

    }


}
