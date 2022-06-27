using System;
using BattleDrakeStudios.ModularCharacters;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static Action<Item> OnItemPickedUp;

    
    public static Action<QuestData> NewQuestAdded;
    public static Action<QuestData> QuestProgressIncreased;
    public static Action<QuestData> OnQuestCompleted;
    public static Action<QuestData,LocalQuestUI> OnRewardClaimed;
    public static Action<QuestData,MainQuestUI> OnMainQuestClaimed;
}
