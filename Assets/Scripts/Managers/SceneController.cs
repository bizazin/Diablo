using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static Action OnGameStarted;

    [SerializeField] private int sceneIndex;

    public static void GameStarter()
    {
        OnGameStarted?.Invoke();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(sceneIndex);
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
