using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static Action OnGameStarted;
    
    public static void GameStarter()
    {
        OnGameStarted?.Invoke();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
        OnGameStarted += StartGame;
    }

    private void OnDisable()
    {
        OnGameStarted -= StartGame;
    }
}
