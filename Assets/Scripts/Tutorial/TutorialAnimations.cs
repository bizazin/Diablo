using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialAnimations : MonoBehaviour
{
  [SerializeField] private Animator animator;
  [SerializeField] private AnimationClip[] clips;
  [SerializeField] private TutorialDialogue[] tutorialDialogues;
  [SerializeField] private Button nextButton;
  [SerializeField] private TextMeshProUGUI name;
  [SerializeField] private TextMeshProUGUI description;

  private bool firstTutorialPassed;
 // [SerializeField] private TutorialDialogue[] tutorialDialogues;

  private int tutorialClipIndex = -1;
  
  private void Start()
  {
    animator = GetComponent<Animator>();
    nextButton.onClick.AddListener(NextTutorialClip);
    if (KeyManager.GetPrefsValue(KeyManager.TutorialPassed) == 0)
        NextTutorialClip();
  }

  public void NextTutorialClip()
  {
      if (tutorialClipIndex<clips.Length-1)
      {
          tutorialClipIndex++;
          animator.Play(clips[tutorialClipIndex].name);
          name.text = tutorialDialogues[tutorialClipIndex].Name;
          description.text = tutorialDialogues[tutorialClipIndex].Description;
      }
      else
      {
          animator.Play("IdleTutorial");
          TutorialPassed();
      }
    
  }
  
  private void TutorialPassed()
  {
      firstTutorialPassed = true;
      KeyManager.SetPrefsValue(KeyManager.TutorialPassed,1);
  }

  public void StartTutorial()
  {
      tutorialClipIndex = -1;
      NextTutorialClip();
  }
  
}
