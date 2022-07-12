using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
public class KeyManager : MonoBehaviour
{
    [SerializeField]
    StringStringDictionary allPrefs;

    public IDictionary<string, string> StringStringDictionary
    {
        get { return allPrefs; }
        set { allPrefs.CopyFrom (value); }
    }
    
    public static Action<string> OnPrefsChanged;
    public static string Name { get => "Name";}
    public static string Coins { get => "Coins";}
    public static string ItemsCount { get => "ItemsCount"; }
    public static string TutorialPassed { get => "TutorialPassed"; }


    private void Awake()
    {
        OnPrefsChanged+=PrefsChanged;
    }

    private void Start()
    {
        //example
        allPrefs.Add(Name,GetValue(Name));
        allPrefs.Add(Coins,GetValue(Coins));
        allPrefs.Add(ItemsCount, GetValue(ItemsCount));
        allPrefs.Add(TutorialPassed, GetValue(TutorialPassed));
    }
    
    public static void SetPrefsValue(string name, int value)
    {
        int curValue = PlayerPrefs.GetInt(name);
        if (value!=curValue)
        {
            PlayerPrefs.SetInt(name,value);
        }
        
        OnPrefsChanged?.Invoke(name);
    }
    
    public static void AddToPrefsValue(string name, int value)
    {
        int curValue = PlayerPrefs.GetInt(name);
        curValue += value;
        if (value!=curValue)
        {
            PlayerPrefs.SetInt(name,value);
        }
        
        OnPrefsChanged?.Invoke(name);
    }
    
    public static int GetPrefsValue(string name)
    {
        return PlayerPrefs.GetInt(name);
    }
    private string GetValue(string name)
    {
        return PlayerPrefs.GetInt(name).ToString();
    }
    private void PrefsChanged(string name)
    {
        if (allPrefs.ContainsKey(name))
        {
            allPrefs[name] =  GetValue(name);
        }
    }
}