using Newtonsoft.Json;
using System.Collections.Generic;
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

    private LocalQuestUI selectedQuest;

    public RemoteConfigStorage Rem;

    private void Start()
    {
        LoadCustomQuests();
    }

    private void LoadCustomQuests()
    {
        Rem = Resources.Load<RemoteConfigStorage>("Storage");
        List<int> loadedJson = new List<int>();

        if (Rem.GetConfig(RemoteConfigs.EnableCustomEquipment).Value == "1")
            loadedJson = JsonConvert.DeserializeObject<List<int>>(Rem.GetConfig(RemoteConfigs.LocalQuests).DefaultValue);
        else
            loadedJson = SaveManager.Instance.LoadJsonList<int>("Quests");

        LoadQuests(loadedJson);
    }

    private void LoadQuests(List<int> loadedJson)
    {
        questsUI = new List<LocalQuestUI>();

        if (loadedJson != null)
            for (int i = 0; i < questsDatasPool.Count; i++)
                for (int j = 0; j < loadedJson.Count; j++)
                    if (questsDatasPool[i].IdQuest == loadedJson[j])
                        AddQuest(questsDatasPool[i]);
    }

    private void OnEnable()
    {
        EventsManager.LocalQuestProgressIncreased += QuestProgressIncreased;
        EventsManager.OnLocalQuestRewardClaimed += ClaimReward;
    }

    public void AddQuest(QuestData quest)
    {
        localQuestPrefab.questData = quest;
        questsUI.Add(localQuestPrefab);
        Instantiate(localQuestPrefab, npcQuestsContainer.transform);
    }

    private void QuestProgressIncreased(int questID)
    {

        QuestData quest = null;
        foreach (var questUI in questsUI)
        {
            if (questUI.questData.IdQuest == questID)
            {
                quest = questUI.questData;
                quest.CurrentProgress++;
            }
        }

        if (quest != null && quest.CurrentProgress >= quest.Goal)
            EventsManager.OnQuestCompleted.Invoke(quest);
    }
    private void ClaimReward(QuestData questData, LocalQuestUI localQuestUI)
    {
        questData.RewardClaimed = true;
        questsUI.Remove(localQuestUI);
        KeyManager.SetPrefsValue(KeyManager.Coins, questData.RewardCoins);
    }

    public void ChangeSelectedQuest(LocalQuestUI currentQuest)
    {
        if (selectedQuest != null && selectedQuest.questData == currentQuest.questData)
        {
            selectedQuest.ToggleSelect();
            selectedQuest = null;
        }
        else if (selectedQuest != null)
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
        EventsManager.LocalQuestProgressIncreased -= QuestProgressIncreased;
        EventsManager.OnLocalQuestRewardClaimed -= ClaimReward;
        SaveQuests();
    }
    private void SaveQuests()
    {
        List<int> questIDs = new List<int>();
        foreach (var quest in questsDatasPool)
        {
            if (quest.QuestTaken && !quest.RewardClaimed)
            {
                questIDs.Add(quest.IdQuest);
            }
        }
        SaveManager.Instance.SaveListToFile("Quests", questIDs);
    }

}
