using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalQuestsManager : MonoBehaviour
{
    [SerializeField]private List<QuestData> questsDatasPool;
    [SerializeField]private List<LocalQuestUI> questsUI;
    [SerializeField]private LocalQuestUI localQuestPrefab;
    [SerializeField]private GameObject npcQuestsContainer;
    private Queue<QuestData> questsData;
    public int currentMainQuestIndex { get; }
    private void Start()
    {
        questsData = new Queue<QuestData>();
        foreach (var questData in questsDatasPool)
        {
            questsData.Enqueue(questData);
        }
        
        EventsManager.NewQuestAdded += AddQuest;
        EventsManager.QuestProgressIncreased +=QuestProgressIncreased;
        EventsManager.OnRewardClaimed += ClaimReward;
    }

    public void AddQuest(QuestData quest)
    {
        var nextQuest = questsData.Dequeue();
        localQuestPrefab.questData = nextQuest;
        questsUI.Add(localQuestPrefab);
        Instantiate(localQuestPrefab,npcQuestsContainer.transform);
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
