using DPUtils.Systems.ItemSystem.Scriptable_Objects.Items.Resources;
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
        {
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public RemoteConfigStorage rem;

    private void Start()
    {
        rem = Resources.Load<RemoteConfigStorage>("Storage");
    }

    public Database LoadDatabase()
    {
        var db = Resources.Load<Database>("DatabaseEquipment");
        return db;
    }

    public void LoadFromFile(string filePath)
    {
        string path = Application.streamingAssetsPath + "/" + filePath + ".json";
        string fileData = File.ReadAllText(path);
        rem.GetConfig(RemoteConfigs.Inventory).Value = fileData;
    }

    public void SaveToFile<T>(string filePath, T data)
    {
        string path = Application.streamingAssetsPath + "/" + filePath + ".json";
        if (!File.Exists(path)) File.Create(path);

        string str = JsonConvert.SerializeObject(data);
        File.WriteAllText(path, str);
    }

    public List<T> LoadJsonList<T>(string filePath, List<T> jsonList)
    {

        jsonList = JsonConvert.DeserializeObject<List<T>>(filePath);
        return jsonList;
    }
}
