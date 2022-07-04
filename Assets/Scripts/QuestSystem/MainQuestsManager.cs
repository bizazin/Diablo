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
        {
            if (!questData.rewardClaimed)
            {
                mainQuestsData.Enqueue(questData);
            }
        }
        TakeNextQuest();
    }
    
    private void LoadQuests()
    {
        
    }

    private void OnEnable()
    {
        EventsManager.MainQuestProgressIncreased +=IncreaseQuestProgress;
        EventsManager.OnMainRewardClaimed += ClaimReward;
        //EventsManager.OnQuestSelected += UnselectQuest;
    }

    private void TakeNextQuest()
    {
        currentQuestData = mainQuestsData.Dequeue();
        mainQuestPrefab.questData = currentQuestData;
        questsUI.Add(mainQuestPrefab);
        currentQuestData.questTaken = true;
        Instantiate(mainQuestPrefab,questsContainer.transform);
    
    }
    public void AddPoint()
    {
        EventsManager.MainQuestProgressIncreased.Invoke(currentQuestData);
    }
    private void IncreaseQuestProgress(QuestData quest)
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
    
    public void ChangeSelectedQuest(MainQuestUI currentQuest)
    {
      //  EventsManager.OnQuestSelected.Invoke();
      
        currentQuest.ToggleSelect();
    }

    public void UnselectQuest()
    {
       questsContainer.transform.GetChild(0).GetComponent<MainQuestUI>().UnselectQuest();
    }
    private void OnDisable()
    {
        EventsManager.MainQuestProgressIncreased -=IncreaseQuestProgress;
        EventsManager.OnMainRewardClaimed -= ClaimReward;
    }
}