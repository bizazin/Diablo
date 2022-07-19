using UnityEngine;
using UnityEngine.SceneManagement;

public class Starter : MonoBehaviour
{
    [SerializeField] private StartConfig startConfig;
    [SerializeField] private CheatWindow cheatWindow;

    private GamePreparer preparer;
    private RemoteConfigStorage rem;

    private void Awake()
    {
        if (!startConfig.ShowCheats)
            SceneManager.LoadScene(1);

        rem = Resources.Load<RemoteConfigStorage>("Storage");
    }

    private void Start()
    {
        Initialization();
    }

    public void Initialization()
    {
        preparer = new GamePreparer(startConfig);
        preparer.Prepare();
        Instantiate(cheatWindow);
    }
}
