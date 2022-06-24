using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsManager : MonoBehaviour
{
    public List<QuestData> questsDatas;
    public List<QuestUI> questsUI;
    public QuestUI questPrefab;
    public GameObject questsContainer;

    private void Start()
    {
        EventsManager.NewQuestAdded += AddQuest;
        EventsManager.QuestProgressIncreased +=QuestProgressIncreased;
        EventsManager.OnRewardClaimed += ClaimReward;
        //EventsManager.OnQuestCompleted += QuestCompleted;
    }

    public void AddQuest(QuestData quest)
    {
        questsDatas.Add(quest);
        questsUI.Add(questPrefab);
        Instantiate(questPrefab,questsContainer.transform);
    }
    // public void QuestCompleted()
    // {
    //     foreach (var i in questsUI)
    //     {
    //         if (i.quest.completed)
    //         {
    //             i.ShowQuestCompleted();
    //         }
    //     }
    // }

    public void QuestProgressIncreased(QuestData quest)
    {
        quest.currentProgress++;
        if (quest.currentProgress>=quest.goal)
        {
            EventsManager.OnQuestCompleted.Invoke();
        }
    }

    
    public void ClaimReward(QuestData questData, QuestUI questUI)
    {
        questData.rewardClaimed = true;
        questsDatas.Remove(questData);
        questsUI.Remove(questUI);
        KeyManager.SetPrefsValue(KeyManager.Coins,questData.rewardCoins);
    }
}
