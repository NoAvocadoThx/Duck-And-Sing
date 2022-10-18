using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    public Button startButton;
    public Button EndButton;

    private bool GameHasStarted = false;
    private bool GameHasEnded = false;
    private bool IsFromRestart = false;
    /**********************************************************************/
    // TODO
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        IsFromRestart = true;

    }
    /**********************************************************************/
    
    public void GameEnd()
    {
        if (!GameHasEnded)
        {
            Time.timeScale = 0f;
            EndButton.gameObject.SetActive(true);
            GameHasEnded = true;
            FindObjectOfType<Duck>().DuckWin();
        }
    }

    private void Awake()
    {
        GameHasEnded = false;
        GameHasStarted = false;
        if (IsFromRestart)
        {
            StartGame();
        }
        else
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
            Time.timeScale = 0f;
            EndButton.gameObject.SetActive(false);
        }
        

        
    }

    private void OnEnable()
    {
        startButton.onClick.AddListener(StartGame);
        EndButton.onClick.AddListener(GameRestart);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(StartGame);
        EndButton.onClick.RemoveListener(GameRestart);
    }

    private void StartGame()
    {
        Time.timeScale = 1f;

        // Hides the button
        startButton.gameObject.SetActive(false);
        GameHasStarted = true;
    }

    public bool IsGameStarted()
    {
        return GameHasStarted;
    }

    public bool IsGameEnded()
    {
        return GameHasEnded;
    }
}
