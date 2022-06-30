using BattleDrakeStudios.ModularCharacters;
using bizazin;
using System;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static Action<Equipment> OnItemPickedUp;
    public static Action<InventorySlot> OnInventorySlotFocused;
    public static Action<Equipment> OnItemEquipped;
    
    //quests
    public static Action<QuestData> OnNewQuestAdded;
    public static Action<QuestData> OnQuestCompleted;
    
    public static Action<QuestData> LocalQuestProgressIncreased;
    public static Action<QuestData,LocalQuestUI> OnLocalQuestRewardClaimed;
    
    public static Action<QuestData> MainQuestProgressIncreased;
    public static Action<QuestData,MainQuestUI> OnMainRewardClaimed;
    
    public static Action OnDeath;
}
