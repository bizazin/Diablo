using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyManager : MonoBehaviour
{
    public static int Money
    {
        get => PlayerPrefs.GetInt("Money");
        set => PlayerPrefs.SetInt("Money", value);
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
