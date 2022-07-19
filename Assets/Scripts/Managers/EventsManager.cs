using System;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static Action<InventorySlot> OnItemClicked;
    public static Action<EquipmentSlot> OnEquipmentClicked;

    public static Action<Equipment> OnItemEquipped;
    public static Action<Equipment> OnItemUnequipped;

    public static Action OnItemDeleted;
    public static Action<Item> OnItemAdded;

    public static Action<QuestData> OnQuestCompleted;

    public static Action<int> LocalQuestProgressIncreased;
    public static Action<QuestData, LocalQuestUI> OnLocalQuestRewardClaimed;

    public static Action<int> MainQuestProgressIncreased;
    public static Action<QuestData, MainQuestUI> OnMainRewardClaimed;

    public static Action OnStatsChanged;
    public static Action<InventorySlot> OnStatsUIChanged;
    public static Action<Equipment> OnUnequippedOrDeletedUI;

    public static Action<PlayerPreview> OnItemEquippedUI;
    public static Action<PlayerPreview> OnItemUnequippedUI;

    public static Action OnCheckingForNewItems;

    public static Action OnDeath;

    public static Action<int> OnPlayerApplyDamage;
}
