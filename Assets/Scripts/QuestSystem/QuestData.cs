using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests")]
public class QuestData : ScriptableObject
{
    public string name;
    public int goal;
    public int currentProgress;
    public string description;
    public bool completed;
    public bool rewardClaimed;
    public int rewardCoins;
}
