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
        SaveManager.Instance.LoadFromFile("Inventory");
        
        itemsStock = SaveManager.Instance.LoadDatabase().Equipment;
        JsonItems = LoadFromRemoteConfig.Instance.LoadJsonList(RemoteConfigs.Inventory, RemoteConfigs.EnableCustomInventory, JsonItems);
        FillList();
    }
    public bool Add(Item item)
    {
        if (Items.Count >= _space)
        {
            Debug.Log("Not enough room");
            return false;
        }
        Items.Add(item);
        JsonItems.Add(item.Name);
        if (OnItemChangedCallback != null) OnItemChangedCallback.Invoke();
       
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
    public void FillList()
    {
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

    private void OnDisable()
    {
        SaveManager.Instance.SaveToFile("Inventory",JsonItems);
    }
}
