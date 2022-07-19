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
            Debug.LogWarning("More than one instance of Inventory is found!");
            return;
        }
        Instance = this;
    }
    #endregion
    
    [SerializeField] private List<QuestData> mainQuestsDatasPool;
    [SerializeField] private List<MainQuestUI> questsUI;
    [SerializeField] private MainQuestUI mainQuestPrefab;
    [SerializeField] private GameObject questsContainer;
    [SerializeField] private QuestData currentQuestData;
    private Queue<QuestData> mainQuestsData;
 
    private void Start()
    {
        mainQuestsData = new Queue<QuestData>();
        foreach (var questData in mainQuestsDatasPool)
            if (!questData.RewardClaimed)
                mainQuestsData.Enqueue(questData);

        TakeNextQuest();
    }

    private void OnEnable()
    {
        EventsManager.MainQuestProgressIncreased +=IncreaseQuestProgress;
        EventsManager.OnMainRewardClaimed += ClaimReward;
    }

    private void TakeNextQuest()
    {
        currentQuestData = mainQuestsData.Dequeue();
        mainQuestPrefab.QuestData = currentQuestData;
        questsUI.Add(mainQuestPrefab);
        currentQuestData.QuestTaken = true;
        Instantiate(mainQuestPrefab,questsContainer.transform);
    }

    private void IncreaseQuestProgress(int questID)
    {
        QuestData quest = null;
        foreach (var questUI in questsUI)
            if (questUI.QuestData.IdQuest == questID)
            {
                quest = questUI.QuestData;
                quest.CurrentProgress++;
            }

        if (quest != null && quest.CurrentProgress>=quest.Goal)
            EventsManager.OnQuestCompleted.Invoke(quest);
    }

    private void ClaimReward(QuestData questData, MainQuestUI mainQuestUI)
    {
        questData.RewardClaimed = true;
        questsUI.Remove(mainQuestUI);
        KeyManager.SetPrefsValue(KeyManager.Coins,questData.RewardCoins);
        TakeNextQuest();
    }
    
    public void ChangeSelectedQuest(MainQuestUI currentQuest)
    {
        currentQuest.ToggleSelect();
    }

    public void UnselectQuest()
    {
       questsContainer.transform.GetChild(0).GetComponent<MainQuestUI>().DeselectQuest();
    }

    private void OnDisable()
    {
        EventsManager.MainQuestProgressIncreased -= IncreaseQuestProgress;
        EventsManager.OnMainRewardClaimed -= ClaimReward;
    }
}