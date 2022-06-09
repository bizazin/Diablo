using UnityEngine;

[System.Serializable]
public class Key
{
    public string KeyName { get; set; }
    public int IntValue
    {
        get => PlayerPrefs.GetInt(KeyName);
        set => PlayerPrefs.SetInt(KeyName, value);
    }

    public string StringValue
    {
        get => PlayerPrefs.GetString(KeyName);
        set => PlayerPrefs.SetString(KeyName, value);
    }

    public string Description { get; set; }

    public Key(string keyName, int intValue, string description)
    {
        KeyName = keyName;
        IntValue = intValue;
        Description = description;
        intValue = IntValue;
    }

    public Key(string keyName, string stringValue, string description)
    {
        KeyName = keyName;
        StringValue = stringValue;
        Description = description;
    }
}
