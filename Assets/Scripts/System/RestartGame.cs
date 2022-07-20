using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour
{
    [SerializeField] private int sceneIndex;
    [SerializeField] private Button restart;
    
    private void OnEnable()
    {
        restart.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ShowWindow()
    {
        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        restart.onClick.RemoveListener(Restart);
    }
}
