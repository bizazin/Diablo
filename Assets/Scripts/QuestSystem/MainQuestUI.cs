using UnityEngine;
using UnityEngine.UI;

public class MainQuestUI : MonoBehaviour
{
   public Slider sliderProgress;
   public Button questCompleted;
   public QuestData questData;

   private void Start()
   {
      sliderProgress.maxValue = questData.goal;
      EventsManager.OnQuestCompleted += ShowQuestCompleted;
      questCompleted.onClick.AddListener(ClaimReward);
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
      EventsManager.OnMainQuestClaimed.Invoke(questData,this);
      Destroy(gameObject);
   }

   private void OnDestroy()
   {
      questCompleted.onClick.RemoveListener(ClaimReward);
   }
}
