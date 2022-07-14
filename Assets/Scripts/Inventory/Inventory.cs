using System;
using System.Collections.Generic;
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
            return;
        Instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;
    [JsonProperty] public List<Item> Items = new List<Item>();
    
    [JsonProperty] public List<string> JsonItems = new List<string>();

    [SerializeField] private int space;
    public int Space { get { return space; } }

    public RemoteConfigStorage rem;
    private void OnEnable()
    {
        rem = Resources.Load<RemoteConfigStorage>("Storage");
    }

    public bool Add(Item item)
    {
        if (!item.IsDefaultItem)
        {
            if (Items.Count >= Space)
            {
                Debug.Log("Not enough room");
                return false;
            }
            Items.Add(item);
            JsonItems.Add(item.Name);

            OnItemChangedCallback?.Invoke();
            KeyManager.SetPrefsValue(KeyManager.ItemsCount, Items.Count);
        }
        return true;
    }

    public void RemoveItem(Item item)
    {
        if (Items.Contains(item))
        {
            Items.Remove(item);
            JsonItems.Remove(item.Name);
        }
        KeyManager.SetPrefsValue(KeyManager.ItemsCount, Items.Count);
        OnItemChangedCallback?.Invoke();
    }
}