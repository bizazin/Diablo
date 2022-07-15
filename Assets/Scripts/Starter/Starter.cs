using UnityEngine;
using UnityEngine.SceneManagement;

public class Starter : MonoBehaviour
{
    [SerializeField] private StartConfig _startConfig;
    [SerializeField] private CheatWindow _cheatWindow;
    private GamePreparer _preparer;
    private RemoteConfigStorage rem;

    private void Awake()
    {
        if (!_startConfig.ShowCheats) 
            SceneManager.LoadScene(1);

        rem = Resources.Load<RemoteConfigStorage>("Storage");
    }

    private void Start()
    {
        Initialization();
    }

    public void Initialization()
    {
        _preparer = new GamePreparer(_startConfig);
        _preparer.Prepare();
        Instantiate(_cheatWindow);
    }
}
