using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainQuestUI : MonoBehaviour
{
   [SerializeField] private Slider sliderProgress;
   [SerializeField] private Button questCompleted;
   [SerializeField] private Button questSelected;
   [SerializeField] private GameObject imageSelected;

   [SerializeField] private TextMeshProUGUI questName;
   [SerializeField] private TextMeshProUGUI questText;
   public QuestData QuestData;

   private void Start()
   {
      SetValues();
      EventsManager.OnQuestCompleted += ShowQuestCompleted;
      questSelected.onClick.AddListener(SelectQuestTarget);
      questCompleted.onClick.AddListener(ClaimReward);
   }

   private void SetValues()
   {
      sliderProgress.maxValue = QuestData.Goal;
      questName.text = QuestData.Name;
      questText.text = QuestData.Description;
   }

   private void Update()
   {
      sliderProgress.value = QuestData.CurrentProgress;

      if (QuestData.Completed)
         questCompleted.gameObject.SetActive(true);
   }
   
   public void ShowQuestCompleted(QuestData quest)
   {
      quest.Completed = true;
   }

   public void ClaimReward()
   {
      EventsManager.OnMainRewardClaimed.Invoke(QuestData,this);
      Destroy(gameObject);
   }
   
   private void SelectQuestTarget()
   {
      ToggleSelect();
      LocalQuestsManager.Instance.UnselectQuest();
      TargetPointer.Instance.Target = QuestData.Target;
   }
   public void ToggleSelect()
   {
      bool state = imageSelected.activeSelf;
      imageSelected.SetActive(!state);
      TargetPointer.Instance.ToggleTarget(!state);
   }

   public void DeselectQuest()
   {
      imageSelected.SetActive(false);
   }

   private void OnDestroy()
   {
      DeselectQuest();
      questCompleted.onClick.RemoveListener(ClaimReward);
   }
}
