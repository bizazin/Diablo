using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory Instance;

    private void Awake()
    {
        if (Instance != null)
            return;
        Instance = this;
    }
    #endregion


    [SerializeField] private int space;
    public int Space { get { return space; } }

    public RemoteConfigStorage rem;
    [JsonProperty] public List<Item> Items = new List<Item>();
    [JsonProperty] public List<string> JsonItems = new List<string>();

    private void OnEnable()
    {
        rem = Resources.Load<RemoteConfigStorage>("Storage");
    }

    public bool Add(Item item)
    {
        if (Items.Count >= Space)
        {
            Debug.Log("Not enough room");
            return false;
        }

        Items.Add(item);
        JsonItems.Add(item.Name);

        EventsManager.OnItemAdded?.Invoke(item);
        KeyManager.SetPrefsValue(KeyManager.ItemsCount, Items.Count);

        return true;
    }

    public void RemoveItem(Item item)
    {
        if (Items.Contains(item))
        {
            Items.Remove(item);
            JsonItems.Remove(item.Name);
        }
        EventsManager.OnItemDeleted?.Invoke();
        KeyManager.SetPrefsValue(KeyManager.ItemsCount, Items.Count);
    }

    public bool CheckForNewItems()
    {
        foreach (var item in Items)
            if (item.IsNew)
                return true;
        return false;
    }
}