using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class LoadFromRemoteConfig : MonoBehaviour
{
    #region Singleton
    public static LoadFromRemoteConfig Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
        rem =Resources.Load<RemoteConfigStorage>("Storage");
    }
    #endregion
    
    private RemoteConfigStorage rem;
    
    public List<T> LoadJsonList<T>(RemoteConfigs type, RemoteConfigs typeToEnable, List<T> jsonList)
    {
    
        string configJson = CheckEnabled(type,typeToEnable);
        
        if (configJson != null)
        {
            jsonList = JsonConvert.DeserializeObject<List<T>>(configJson);
        }
        else
        {
            configJson = rem.GetConfig(RemoteConfigs.Inventory).DefaultValue;
            jsonList = JsonConvert.DeserializeObject<List<T>>(configJson);
        }

        return jsonList;
    }
    
    private string CheckEnabled(RemoteConfigs type,RemoteConfigs typeToEnable)
    {
        string configJson = "";
        if (rem.GetConfig(typeToEnable).Value == "1")
        {
            configJson = rem.GetConfig(type).DefaultValue;
        }
        else
        {
            configJson = rem.GetConfig(type).Value;
        }

        return configJson;
    }
    
}

