using System;
using System.Collections.Generic;
using bizazin;
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
            Debug.LogWarning("More than one instance of Inventory is found!");
            return;
        }
        Instance = this;
    }

    #endregion
    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;
    [JsonProperty] public List<Item> Items = new List<Item>();
    
    [JsonProperty] public List<string> JsonItems = new List<string>();



    [SerializeField] private int _space;

    public RemoteConfigStorage rem;
    private void OnEnable()
    {
        rem = Resources.Load<RemoteConfigStorage>("Storage");
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
            JsonItems.Add(item.ItemName);

            if (OnItemChangedCallback != null)
                OnItemChangedCallback.Invoke();
        }
        return true;
    }
    public void Remove(Item item)
    {
        if (Items.Contains(item))
        {
            Items.Remove(item);
            JsonItems.Remove(item.ItemName);
        }
        if (OnItemChangedCallback != null) 
            OnItemChangedCallback.Invoke();
    }
}
