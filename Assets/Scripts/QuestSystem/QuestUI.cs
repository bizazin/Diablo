using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
   public Slider sliderProgress;
   public Button questCompleted;
   public QuestData quest;

   private void Start()
   {
      sliderProgress.maxValue = quest.goal;
      EventsManager.OnQuestCompleted += ShowQuestCompleted;
      questCompleted.onClick.AddListener(ClaimReward);
   }

   private void Update()
   {
      sliderProgress.value = quest.currentProgress;
   }

   public void ShowQuestCompleted()
   {
      quest.completed = true;
      questCompleted.gameObject.SetActive(true);
   }

   public void ClaimReward()
   {
      EventsManager.OnRewardClaimed.Invoke(quest,this);
      Destroy(gameObject);
   }

   private void OnDestroy()
   {
      questCompleted.onClick.RemoveListener(ClaimReward);
   }
}
