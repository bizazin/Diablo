using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class CheatWindow : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject container;
    
    private bool opened;

    private void Awake()
    {
        opened = true;
        UpdateView(opened);
        
    }
    
    public void SetActive(bool value)
    {
        if (container != null)
        {
            container.SetActive(value);
        }
    }
    
    private void SwitchState()
    {
        opened = !opened;
        UpdateView(opened);
    }
    
    private void UpdateView(bool value)
    {
        SetActive(value);
    }

    public void StartGame()
    {
        SceneController.OnGameStarted?.Invoke();
    }
    
    private void OnEnable()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartGame);
        }

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(SwitchState);
        }
    }

    private void OnDisable()
    {
        if(startButton != null)
        {
            startButton.onClick.RemoveListener(StartGame);
        }

        if (closeButton != null)
        {
            closeButton.onClick.RemoveListener(SwitchState);
        }
    }


    
}
