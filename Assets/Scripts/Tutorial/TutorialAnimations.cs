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

    private int tutorialClipIndex;

    private void Start()
    {
        tutorialClipIndex = -1;
        animator = GetComponent<Animator>();
        nextButton.onClick.AddListener(NextTutorialClip);
        if (KeyManager.GetPrefsValue(KeyManager.TutorialPassed) == 0)
            NextTutorialClip();
    }

    private void TutorialPassed()
    {
        KeyManager.SetPrefsValue(KeyManager.TutorialPassed, 1);
    }

    public void NextTutorialClip()
    {
        if (tutorialClipIndex < clips.Length - 1)
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

    public void StartTutorial()
    {
        tutorialClipIndex = -1;
        NextTutorialClip();
    }

}
