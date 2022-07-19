using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    #region Singleton
    public static SaveManager Instance;

    private void Awake()
    {
        if (Instance != null)
            return;
        Instance = this;
    }
    #endregion

    public RemoteConfigStorage Rem;

    private void Start()
    {
        Rem = Resources.Load<RemoteConfigStorage>("Storage");
    }

    public void SaveToFile<T>(string filePath, T[] data)
    {
        string path = Application.streamingAssetsPath + "/" + filePath + ".json";
        if (!File.Exists(path)) File.Create(path);

        string str = JsonConvert.SerializeObject(data, Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        File.WriteAllText(path, str);
    }

    public void SaveListToFile<T>(string filePath, List<T> data)
    {
        string path = Application.streamingAssetsPath + "/" + filePath + ".json";
        if (!File.Exists(path)) File.Create(path);

        string str = JsonConvert.SerializeObject(data);
        File.WriteAllText(path, str);
    }

    public T[] LoadJsonArray<T>(string filePath, T[] jsonList)
    {
        string path = Application.streamingAssetsPath + "/" + filePath + ".json";
        var str = File.ReadAllText(path);
        jsonList = JsonConvert.DeserializeObject<T[]>(str,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        return jsonList;
    }


    public List<int> LoadJsonList<T>(string filePath)
    {
        string path = Application.streamingAssetsPath + "/" + filePath + ".json";
        var str = File.ReadAllText(path);
        var jsonList = JsonConvert.DeserializeObject<List<int>>(str,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        return jsonList;
    }

    // public List<T> LoadJsonListFromRem<T>(RemoteConfigs remoteConfig)
    // {
    //     if (rem.GetConfig(remoteConfig)!=null)
    //     {
    //         var jsonString = rem.GetConfig(remoteConfig).Value;
    //         var jsonList = JsonConvert.DeserializeObject<List<T>>(jsonString);
    //         return jsonList;
    //     }
    //     return new List<T>();
    // }
}
