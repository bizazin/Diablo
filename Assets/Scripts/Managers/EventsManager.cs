using System;
using UnityEngine;
using UnityEngine.Events;

public class EventsManager : MonoBehaviour
{
    public static Action<Equipment> OnItemPickedUp;
    public static Action<InventorySlot> OnItemClicked;
    public static Action<EquipmentSlot> OnEquipmentClicked;
    public static Action<Equipment> OnItemEquipped;
    public static Action<Equipment> OnItemUnequipped;


    //quests
    public static Action<QuestData> OnNewQuestAdded;
    public static Action<QuestData> OnQuestCompleted;

    public static Action<QuestData> LocalQuestProgressIncreased;
    public static Action<QuestData, LocalQuestUI> OnLocalQuestRewardClaimed;

    public static Action<QuestData> MainQuestProgressIncreased;
    public static Action<QuestData, MainQuestUI> OnMainRewardClaimed;
    
    public static Action OnStatsChanged;
    public static Action<InventorySlot> OnStatsUIChanged;
    
    public static Action OnDeath;

    public static Action<int> OnPlayerApplyDamage;
}
