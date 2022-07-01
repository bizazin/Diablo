using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalQuestUI : MonoBehaviour
{
   [SerializeField] private Slider sliderProgress;
   [SerializeField] private Button questCompleted;
   [SerializeField] private Button questSelected;
   [SerializeField] private GameObject imageSelected;
   
   [SerializeField] private TextMeshProUGUI questName;
   [SerializeField] private TextMeshProUGUI questText;
   public QuestData questData;

   private void Start()
   {
      SetValues();
      EventsManager.OnQuestCompleted += ShowQuestCompleted;
      questCompleted.onClick.AddListener(ClaimReward);
      questSelected.onClick.AddListener(SelectQuestTarget);
   }
   
   private void SetValues()
   {
      sliderProgress.maxValue = questData.goal;
      questName.text = questData.name;
      questText.text = questData.description;
   }


   private void Update()
   {
      sliderProgress.value = questData.currentProgress;

      if (questData.completed)
         questCompleted.gameObject.SetActive(true);
   }

   private void ShowQuestCompleted(QuestData quest)
   {
      quest.completed = true;
   }

   private void SelectQuestTarget()
   {
      
      //ToggleSelect();
      MainQuestsManager.Instance.UnselectQuest();
      LocalQuestsManager.Instance.ChangeSelectedQuest(this);
      TargetPointer.Instance.Target = questData.target;
      //LocalQuestsManager.Instance.ChangeSelectedQuest(this);
     // EventsManager.OnQuestSelected.Invoke(this);
   }
   public void ToggleSelect()
   {
      bool state = imageSelected.activeSelf;
      imageSelected.SetActive(!state);
      TargetPointer.Instance.ToggleTarget(!state);
   }

   public void UnselectQuest()
   {
      imageSelected.SetActive(false);
   }

   private void ClaimReward()
   {
      EventsManager.OnLocalQuestRewardClaimed.Invoke(questData,this);
      Destroy(gameObject);
   }

   private void OnDestroy()
   {
      UnselectQuest();
      questCompleted.onClick.RemoveListener(ClaimReward);
      questSelected.onClick.RemoveListener(SelectQuestTarget);
   }
}
