using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class LocalQuestUI : MonoBehaviour
{
   [JsonIgnore][SerializeField] private Slider sliderProgress;
   [JsonIgnore][SerializeField] private Button questCompleted;
   [JsonIgnore][SerializeField] private Button questSelected;
   [JsonIgnore][SerializeField] private GameObject imageSelected;
   
   [JsonIgnore][SerializeField] private TextMeshProUGUI questName;
   [JsonIgnore][SerializeField] private TextMeshProUGUI questText;
   [JsonIgnore]public QuestData questData;
   [JsonProperty] public int i = 012;

   private void Start()
   {
      SetValues();
      EventsManager.OnQuestCompleted += ShowQuestCompleted;
      questCompleted.onClick.AddListener(ClaimReward);
      questSelected.onClick.AddListener(SelectQuestTarget);
   }
   
   private void SetValues()
   {
      sliderProgress.maxValue = questData.Goal;
      questName.text = questData.Name;
      questText.text = questData.Description;
   }


   private void Update()
   {
      sliderProgress.value = questData.CurrentProgress;

      if (questData.Completed)
         questCompleted.gameObject.SetActive(true);
   }

   private void ShowQuestCompleted(QuestData quest)
   {
      quest.Completed = true;
   }

   private void SelectQuestTarget()
   {
      
      //ToggleSelect();
      MainQuestsManager.Instance.UnselectQuest();
      LocalQuestsManager.Instance.ChangeSelectedQuest(this);
      TargetPointer.Instance.Target = questData.Target;
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
