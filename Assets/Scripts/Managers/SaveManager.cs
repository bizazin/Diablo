using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public RemoteConfigStorage rem;

    private void Start()
    {
        rem = Resources.Load<RemoteConfigStorage>("Storage");
    }
    public void LoadFromFile(string filePath, RemoteConfigs type)
    {
        string path = Application.streamingAssetsPath + "/" + filePath + ".json";
        string fileData = File.ReadAllText(path);
        rem.GetConfig(type).Value = fileData;
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
}