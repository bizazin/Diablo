using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests")]
public class QuestData : ScriptableObject
{
    public int IdQuest;
    public string Name;
    public int Goal;
    public int CurrentProgress;
    public string Description;
    public bool QuestTaken;
    public bool Completed;
    public bool RewardClaimed;
    public int RewardCoins;

    public Transform Target;
}
