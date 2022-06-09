using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class CheatWindow : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private GameObject _container;
    
    private bool _opened;

    private void Awake()
    {
        _opened = true;
        UpdateView(_opened);
    }
    
    public void SetActive(bool value)
    {
        if (_container != null)
            _container.SetActive(value);
    }
    
    private void SwitchState()
    {
        _opened = !_opened;
        UpdateView(_opened);
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
        if (_startButton != null)
            _startButton.onClick.AddListener(StartGame);

        if (_closeButton != null)
            _closeButton.onClick.AddListener(SwitchState);
    }

    private void OnDisable()
    {
        if(_startButton != null)
            _startButton.onClick.RemoveListener(StartGame);

        if (_closeButton != null)
            _closeButton.onClick.RemoveListener(SwitchState);
    }
}
