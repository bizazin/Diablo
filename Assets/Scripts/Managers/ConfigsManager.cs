using UnityEngine;

public class ConfigsManager : MonoBehaviour
{
    public RemoteConfigStorage remoteConfigs;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
