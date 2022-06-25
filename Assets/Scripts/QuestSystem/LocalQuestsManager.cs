using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalQuestsManager : MonoBehaviour
{
    #region Singleton

    public static LocalQuestsManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
    }

    #endregion
    
    [SerializeField]private List<QuestData> questsDatas;
    [SerializeField]private List<QuestUI> questsUI;
    [SerializeField]private QuestUI questPrefab;
    [SerializeField]private GameObject questsContainer;
    public int currentMainQuestIndex { get; }
    private void Start()
    {
        EventsManager.NewQuestAdded += AddQuest;
        EventsManager.QuestProgressIncreased +=QuestProgressIncreased;
        EventsManager.OnRewardClaimed += ClaimReward;
    }

    public void AddQuest(QuestData quest)
    {
        questsDatas.Add(quest);
        questsUI.Add(questPrefab);
        Instantiate(questPrefab,questsContainer.transform);
    }
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
