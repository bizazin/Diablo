using System;
using UnityEngine;

[Serializable]
public class RemoteConfigData
{
    [SerializeField] private string name;
    [SerializeField] private RemoteConfigs type;
    [SerializeField] private string defaultValue;

    private string remoteValue;

    public string Name => name;
    public RemoteConfigs Type => type;
    public string DefaultValue => defaultValue;

    public string Value
    {
        get => string.IsNullOrEmpty(remoteValue) ? defaultValue : remoteValue;
        set => remoteValue = value;
    }

}
