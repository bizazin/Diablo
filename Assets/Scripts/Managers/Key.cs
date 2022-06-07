using UnityEngine;

[System.Serializable]
public class Key
{
    public string keyName;

    public int intValueee; 

    public int intValue
    {
        get => PlayerPrefs.GetInt(keyName);
        set => PlayerPrefs.SetInt(keyName, value);
    }

    public string stringValue
    {
        get => PlayerPrefs.GetString(keyName);
        set => PlayerPrefs.SetString(keyName , value);
    }

    public string description;

    public Key(string keyName , int intValue, string description)
    {
        this.keyName = keyName;
        this.intValue = intValue;
        this.description = description;
        intValueee = this.intValue;
    }

    public Key(string keyName, string stringValue, string description)
    {
        this.keyName = keyName;
        this.stringValue = stringValue;
        this.description = description;
    }
}
