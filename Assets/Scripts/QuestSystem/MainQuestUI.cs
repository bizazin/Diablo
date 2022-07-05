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
   public QuestData questData;

   private void Start()
   {
      SetValues();
      EventsManager.OnQuestCompleted += ShowQuestCompleted;
      questSelected.onClick.AddListener(SelectQuestTarget);
      questCompleted.onClick.AddListener(ClaimReward);
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
   
   public void ShowQuestCompleted(QuestData quest)
   {
      quest.completed = true;
   }

   public void ClaimReward()
   {
      EventsManager.OnMainRewardClaimed.Invoke(questData,this);
      Destroy(gameObject);
   }
   
   private void SelectQuestTarget()
   {
      ToggleSelect();
      LocalQuestsManager.Instance.UnselectQuest();
      TargetPointer.Instance.Target = questData.target;
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

   
   private void OnDestroy()
   {
      UnselectQuest();
      questCompleted.onClick.RemoveListener(ClaimReward);
   }
}
