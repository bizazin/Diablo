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
    [JsonProperty] public List<IteM> Items = new List<IteM>();
    [JsonProperty] public List<string> JsonItems = new List<string>();

    [SerializeField] private int _space;
    public RemoteConfigStorage rem;
    public IteM item;
    private void OnEnable()
    {
        rem = Resources.Load<RemoteConfigStorage>("Storage");
        
       

    }
    public bool Add(IteM item)
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
    public void Remove(IteM item)
    {
        if (Items.Contains(item))
        {
            Items.Remove(item);
            JsonItems.Remove(item.Name);
        }
        if (OnItemChangedCallback != null) OnItemChangedCallback.Invoke();
    }


    private void OnDisable()
    {
       // SaveManager.Instance.SaveToFile("Inventory",JsonItems);
    }
}
