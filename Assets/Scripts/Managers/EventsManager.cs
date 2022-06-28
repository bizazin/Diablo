using System;
using BattleDrakeStudios.ModularCharacters;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static Action<Item> OnItemPickedUp;
    
    //quests
    public static Action<QuestData> OnNewQuestAdded;
    public static Action<QuestData> OnQuestCompleted;
    
    public static Action<QuestData> LocalQuestProgressIncreased;
    public static Action<QuestData,LocalQuestUI> OnLocalQuestRewardClaimed;
    
    public static Action<QuestData> MainQuestProgressIncreased;
    public static Action<QuestData,MainQuestUI> OnMainRewardClaimed;
}
