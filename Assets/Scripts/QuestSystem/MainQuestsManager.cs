using System.Collections.Generic;
using UnityEngine;

public class MainQuestsManager : MonoBehaviour
{
    [SerializeField] private List<QuestData> mainQuestsDatasPool;
    [SerializeField] private List<MainQuestUI> questsUI;
    [SerializeField] private MainQuestUI mainQuestPrefab;
    [SerializeField] private GameObject questsContainer;
    [SerializeField] private QuestData currentQuest;
    private Queue<QuestData> mainQuestsData;
 
    private void Start()
    {
        mainQuestsData = new Queue<QuestData>();
        foreach (var questData in mainQuestsDatasPool)
        {
            mainQuestsData.Enqueue(questData);
        }
        TakeNextQuest();
    }

    private void OnEnable()
    {
        EventsManager.MainQuestProgressIncreased +=QuestProgressIncreased;
        EventsManager.OnMainRewardClaimed += ClaimReward;
    }

    private void TakeNextQuest()
    {
        currentQuest = mainQuestsData.Dequeue();
        mainQuestPrefab.questData = currentQuest;
        questsUI.Add(mainQuestPrefab);
        Instantiate(mainQuestPrefab,questsContainer.transform);
    
    }
    public void AddPoint()
    {
        EventsManager.MainQuestProgressIncreased.Invoke(currentQuest);
    }
    private void QuestProgressIncreased(QuestData quest)
    {
        quest.currentProgress++;
        if (quest.currentProgress>=quest.goal)
        {
            EventsManager.OnQuestCompleted.Invoke(quest);
        }
    }

    private void ClaimReward(QuestData questData, MainQuestUI mainQuestUI)
    {
        questData.rewardClaimed = true;
        questsUI.Remove(mainQuestUI);
        KeyManager.SetPrefsValue(KeyManager.Coins,questData.rewardCoins);
        TakeNextQuest();
    }

    private void OnDisable()
    {
        EventsManager.MainQuestProgressIncreased -=QuestProgressIncreased;
        EventsManager.OnMainRewardClaimed -= ClaimReward;
    }
}