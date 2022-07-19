using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> Waypoints;
    private StateController[] controllers;

    private void Start()
    {
        controllers = FindObjectsOfType<StateController>();
        foreach (var controller in controllers)
            controller.InitializeAI(true, Waypoints);
    }
}
