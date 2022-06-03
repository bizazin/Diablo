using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        Instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;
    [JsonProperty] public List<Item> Items = new List<Item>();
    [JsonProperty] public List<string> JsonItems = new List<string>();

    public List<Equipment> itemsStock;

    [SerializeField] private int _space;
    public RemoteConfigStorage rem;
    public Item item;
    private void OnEnable()
    {
        rem = Resources.Load<RemoteConfigStorage>("Storage");
        LoadFromFile();
        AvailableItems();
        LoadSavesFromConfig();
    }
    public bool Add(Item item)
    {
        if (!item.IsDefaultItem)
        {
            if (Items.Count >= _space)
            {
                Debug.Log("Not enough room");
                return false;
            }
            Items.Add(item);
            JsonItems.Add(item.Name);
             if (OnItemChangedCallback != null) OnItemChangedCallback.Invoke();
        }
        return true;
    }
    
    public void Remove(Item item)
    {
        if (Items.Contains(item))
        {
            Items.Remove(item);
            JsonItems.Remove(item.Name);
        }
        if (OnItemChangedCallback != null) OnItemChangedCallback.Invoke();
    }
    
    private void AvailableItems()
    {
        itemsStock.Add(Resources.Load<Equipment>("Hood"));
        itemsStock.Add(Resources.Load<Equipment>("KnightArmor"));
        itemsStock.Add(Resources.Load<Equipment>("KnightBoots"));
        itemsStock.Add(Resources.Load<Equipment>("KnightHelmet"));
        itemsStock.Add(Resources.Load<Equipment>("LeatherBoots"));
        itemsStock.Add(Resources.Load<Equipment>("LeatherArmor"));
    }
    private void LoadSavesFromConfig()
    {
        string inventoryJson = "";
        if (rem.GetConfig(RemoteConfigs.EnableCustomInventory).Value=="1")
        {
            inventoryJson = rem.GetConfig(RemoteConfigs.Inventory).DefaultValue;
        }
        else
        { 
            inventoryJson = rem.GetConfig(RemoteConfigs.Inventory).Value;
        }
        if (inventoryJson!=null)
        {
            List<string> temp = JsonConvert.DeserializeObject<List<string>>(inventoryJson);
            JsonItems = temp;
        }
        else{
            inventoryJson = rem.GetConfig(RemoteConfigs.Inventory).DefaultValue;
            List<string> temp = JsonConvert.DeserializeObject<List<string>>(inventoryJson);
            JsonItems = temp;
        }
        if (JsonItems != null)
            for (int i = 0; i < JsonItems.Count; i++)
            {
                for (int j = 0; j < itemsStock.Count; j++)
                {
                    if (itemsStock[j].name == JsonItems[i])
                    {
                        Items.Add(itemsStock[j]);
                    }
                }
            }
    }

    public void LoadFromFile()
    {
        string path = Application.streamingAssetsPath+"/Inventory.json";
        string fileData = File.ReadAllText(path);
        rem.GetConfig(RemoteConfigs.Inventory).Value = fileData;
    }
    public void SaveToFile()
    {
        string path = Application.streamingAssetsPath+"/Inventory.json";
        string str = JsonConvert.SerializeObject(JsonItems);
        File.WriteAllText(path,str);
    }
    
    private void OnDisable()
    {
        SaveToFile();
    }
}
