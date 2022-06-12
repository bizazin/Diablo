using BattleDrakeStudios.ModularCharacters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    #region Singleton

    public static EventManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
    }

    #endregion

    public UnityEvent OnDefaultEvent;

    public UnityEvent<Item> OnItemPickedUp;
}
