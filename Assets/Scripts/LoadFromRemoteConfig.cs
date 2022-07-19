using Newtonsoft.Json;
using System.Collections.Generic;
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
        rem = Resources.Load<RemoteConfigStorage>("Storage");
    }
    #endregion

    private RemoteConfigStorage rem;

    public void SaveJsonListToRem<T>(List<T> list, RemoteConfigs remoteConfig)
    {
        string jsonString = JsonConvert.SerializeObject(list);
        rem.GetConfig(remoteConfig).Value = jsonString;
    }

    public List<T> LoadJsonListCustom<T>(RemoteConfigs type, RemoteConfigs typeToEnable, List<T> jsonList)
    {

        string configJson = CheckEnabled(type, typeToEnable);

        if (configJson != null)
            jsonList = JsonConvert.DeserializeObject<List<T>>(configJson);
        else
        {
            configJson = rem.GetConfig(type).DefaultValue;
            jsonList = JsonConvert.DeserializeObject<List<T>>(configJson);
        }

        return jsonList;
    }

    public List<T> LoadJsonList<T>(RemoteConfigs type)
    {
        string configJson = rem.GetConfig(type).Value;
        var jsonList = JsonConvert.DeserializeObject<List<T>>(configJson);
        return jsonList;
    }

    private string CheckEnabled(RemoteConfigs type, RemoteConfigs typeToEnable)
    {
        string configJson = string.Empty;
        if (rem.GetConfig(typeToEnable).Value == "1")
            configJson = rem.GetConfig(type).DefaultValue;
        else
            configJson = rem.GetConfig(type).Value;

        return configJson;
    }
}

