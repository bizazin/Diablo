using UnityEngine;
using UnityEngine.SceneManagement;

public class Starter : MonoBehaviour
{
    [SerializeField] private StartConfig _startConfig;
    [SerializeField] private CheatWindow _cheatWindow;
    private GamePreparer _preparer;
    public RemoteConfigStorage rem;
    private void Awake()
    {
        if (!_startConfig.ShowCheats) 
            SceneManager.LoadScene("SampleScene");

        rem = Resources.Load<RemoteConfigStorage>("Storage");
    }

    private void Start()
    {
        Initialization();
      //  var str = ConfigsManager.remoteConfigs.GetConfig(RemoteConfigs.Inventory).Value;
       // var str = rem.GetConfig(RemoteConfigs.Inventory).Value;
      // var str = rem.GetConfig(RemoteConfigs.Inventory).Value;
      var lol = 0;
    }

    public void Initialization()
    {
        _preparer = new GamePreparer(_startConfig);
        _preparer.Prepare();
        Instantiate(_cheatWindow);
    }
}
