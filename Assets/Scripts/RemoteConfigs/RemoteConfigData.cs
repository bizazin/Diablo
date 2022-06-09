using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RemoteConfigData
{
    [SerializeField] private string _name;
    [SerializeField] private RemoteConfigs _type;
    [SerializeField] private string _defaultValue;

    private string _remoteValue;

    public string Name => _name;
    public RemoteConfigs Type => _type;
    public string DefaultValue => _defaultValue;
    
    public string Value
    {
        get => string.IsNullOrEmpty(_remoteValue) ? _defaultValue : _remoteValue;
        set => _remoteValue = value;
    }
    
}
