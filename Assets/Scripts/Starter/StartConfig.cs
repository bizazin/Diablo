using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Start Config", menuName = "ScriptableObjects/Start Config", order = 1)]
public class StartConfig : ScriptableObject
{
  public bool ClearPrefs = false;
  public bool ClearCache = false;
  public bool ShowCheats = false;
  public int TargetFrameRate = 30;
}
