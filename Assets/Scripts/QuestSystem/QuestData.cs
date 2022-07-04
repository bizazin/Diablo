using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests")]
public class QuestData : ScriptableObject
{
    public int idQuest;
    public string name;
    public int goal;
    public int currentProgress;
    public string description;
    public bool questTaken;
    public bool completed;
    public bool rewardClaimed;
    public int rewardCoins;

    public Transform target;
}
