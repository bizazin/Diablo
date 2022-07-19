using UnityEngine;

[CreateAssetMenu(fileName = "Storage", menuName = "Remote")]
public class RemoteConfigStorage : ScriptableObject
{
    [SerializeField] private RemoteConfigData[] configs;

    public RemoteConfigData[] Configs
    {
        get { return configs; }
        set { configs = value; }
    }

    public RemoteConfigData GetConfig(RemoteConfigs type)
    {
        foreach (var conf in Configs)
            if (conf.Type == type)
                return conf;
        return null;
    }
}


