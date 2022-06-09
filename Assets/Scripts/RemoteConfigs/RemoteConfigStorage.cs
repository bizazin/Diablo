using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Storage", menuName = "Remote")]
public class RemoteConfigStorage : ScriptableObject
{
    [SerializeField] private RemoteConfigData[] _configs;

    public RemoteConfigData[] Configs
    {
        get { return _configs;}
        set { _configs = value; }
    }
    
    public RemoteConfigData GetConfig(RemoteConfigs type)
    {
        for (int i = 0; i < Configs.Length; i++)
        {
            if (Configs[i].Type == type)
                return Configs[i];
        }
        return null;
    }
}


