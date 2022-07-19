using UnityEngine;

[CreateAssetMenu(fileName = "Start Config", menuName = "ScriptableObjects/Start Config", order = 1)]
public class StartConfig : ScriptableObject
{
    public bool ClearPrefs;
    public bool ClearCache;
    public bool ShowCheats;
    public int TargetFrameRate = 30;
}
