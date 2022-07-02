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
        {
            Debug.LogWarning("More than one instance of Inventory is found!");
            return;
        }
        Instance = this;
    }
    #endregion
    
    [SerializeField] private List<QuestData> questsDatasPool;
    [JsonProperty][SerializeField] private List<LocalQuestUI> questsUI;
    [SerializeField] private LocalQuestUI localQuestPrefab;
    [SerializeField] private GameObject npcQuestsContainer;
    private Queue<QuestData> questsData;
    public LocalQuestUI selectedQuest;
    public RemoteConfigStorage rem;
    private void Start()
    {
        questsData = new Queue<QuestData>();
       // questsUI = SaveManager.Instance.LoadJsonList<LocalQuestUI>("Quests");
        foreach (var questData in questsDatasPool)
        {
            questsData.Enqueue(questData);
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
        var nextQuest = questsData.Dequeue();
        localQuestPrefab.questData = nextQuest;
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
      //  EventsManager.OnQuestSelected.Invoke();
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
        //LoadFromRemoteConfig.Instance.SaveJsonListToRem(questsUI, RemoteConfigs.LocalQuests);
        //SaveManager.Instance.SaveListToFile("Quests",questsUI);
    }
}
