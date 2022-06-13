using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigsManager : MonoBehaviour
{
    public RemoteConfigStorage remoteConfigs;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
