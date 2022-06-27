using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainQuestsManager : MonoBehaviour
{
    #region Singleton

    public static MainQuestsManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
    }

    #endregion
    
    [SerializeField]private List<QuestData> questsDatasPool;
    [SerializeField]private List<LocalQuestUI> questsUI;
    [SerializeField]private LocalQuestUI mainQuestPrefab;
    [SerializeField]private GameObject questsContainer;
    private Queue<QuestData> questsData;
    public int currentMainQuestIndex { get; }
    private void Start()
    {
        questsData = new Queue<QuestData>();
        foreach (var questData in questsDatasPool)
        {
            questsData.Enqueue(questData);
        }
        
        // EventsManager.NewQuestAdded += AddQuest;
        EventsManager.QuestProgressIncreased +=QuestProgressIncreased;
        EventsManager.OnRewardClaimed += ClaimReward;
    }

    public void ChangeQuest(QuestData quest)
    {
        var nextQuest = questsData.Dequeue();
        mainQuestPrefab.questData = nextQuest;
        questsUI.Add(mainQuestPrefab);
        Instantiate(mainQuestPrefab,questsContainer.transform);
    }
    public void QuestProgressIncreased(QuestData quest)
    {
        quest.currentProgress++;
        if (quest.currentProgress>=quest.goal)
        {
            EventsManager.OnQuestCompleted.Invoke(quest);
        }
    }

    public void ClaimReward(QuestData questData, LocalQuestUI localQuestUI)
    {
        questData.rewardClaimed = true;
        questsUI.Remove(localQuestUI);
        KeyManager.SetPrefsValue(KeyManager.Coins,questData.rewardCoins);
    }
}