using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class LocalQuestsManager : MonoBehaviour, ICanBeSaved
{
    #region Singleton
    public static LocalQuestsManager Instance;

    private void Awake()
    {
        if (Instance != null)
            return;
        Instance = this;
    }
    #endregion
    
    [SerializeField] private List<QuestData> questsDatasPool;
    [SerializeField] private List<LocalQuestUI> questsUI;
    [SerializeField] private LocalQuestUI localQuestPrefab;
    [SerializeField] private GameObject npcQuestsContainer;
    public LocalQuestUI selectedQuest;
    private void Start()
    {
        LoadQuests();
    }

    private void LoadQuests()
    {
        List<int> loadedJson = SaveManager.Instance.LoadJsonList<int>("Quests");
        questsUI = new List<LocalQuestUI>();
        if (loadedJson != null)
        {
            for (int i=0; i<questsDatasPool.Count;i++)
            {
                for (int j = 0; j < loadedJson.Count; j++)
                {
                    if (questsDatasPool[i].idQuest == loadedJson[j])
                    {
                        AddQuest(questsDatasPool[i]);
                    };
                }
            }
        }
    }

    private void OnEnable()
    {        
        EventsManager.OnNewQuestAdded += AddQuest;
        EventsManager.LocalQuestProgressIncreased +=QuestProgressIncreased;
        EventsManager.OnLocalQuestRewardClaimed += ClaimReward;
    }

    public void AddQuest(QuestData quest)
    {
        localQuestPrefab.questData = quest;
        questsUI.Add(localQuestPrefab);
        Instantiate(localQuestPrefab,npcQuestsContainer.transform);
    }

    private void QuestProgressIncreased(QuestData quest)
    {
        quest.currentProgress++;
        if (quest.currentProgress>=quest.goal)
        {
            EventsManager.OnQuestCompleted.Invoke(quest);
        }
    }
    private void ClaimReward(QuestData questData, LocalQuestUI localQuestUI)
    {
        questData.rewardClaimed = true;
        questsUI.Remove(localQuestUI);
        KeyManager.SetPrefsValue(KeyManager.Coins,questData.rewardCoins);
    }

    public void ChangeSelectedQuest(LocalQuestUI currentQuest)
    {
        if (selectedQuest != null && selectedQuest.questData == currentQuest.questData)
        {
            selectedQuest.ToggleSelect();
            selectedQuest = null;
        }
        else if(selectedQuest!=null)
        {
            selectedQuest.ToggleSelect();
            selectedQuest = currentQuest;
            selectedQuest.ToggleSelect();
        }
        else
        {
            selectedQuest = currentQuest;
            selectedQuest.ToggleSelect();
        }
    }

    public void UnselectQuest()
    {
        if (selectedQuest != null)
        {
            selectedQuest.UnselectQuest();
            selectedQuest = null;
        }
    }

    private void OnDisable()
    {
        EventsManager.OnNewQuestAdded -= AddQuest;
        EventsManager.LocalQuestProgressIncreased -=QuestProgressIncreased;
        EventsManager.OnLocalQuestRewardClaimed -= ClaimReward;
        SaveQuests();
    }
    private void SaveQuests()
    {
        List<int> questIDs = new List<int>();
        foreach (var quest in questsDatasPool)
        {
            if (quest.questTaken && !quest.rewardClaimed)
            {
                questIDs.Add(quest.idQuest);
            }
        }
        SaveManager.Instance.SaveListToFile("Quests", questIDs);
    }
    
}
