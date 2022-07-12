using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private List<DialogueData> allDialogues;
    
    private Queue<DialogueData> dialoguesQueue;
    private Queue<string> sentences;
    public QuestData questData;
    
    private string isOpen = "IsOpen";
    
    private void Start()
    {
        sentences = new Queue<string>();
        dialoguesQueue = new Queue<DialogueData>();
    }
    public void AddQuest()
    {
        LocalQuestsManager.Instance.AddQuest(questData);
    }
    public void AddPointQuest()
    {
        EventsManager.LocalQuestProgressIncreased.Invoke(questData);
    }
    public void EnqueueDialogues(DialogueData.Character character)
    {
        foreach (var dialogue in allDialogues)
            if(dialogue.character == character && !dialogue.quest.completed)
                dialoguesQueue.Enqueue(dialogue);
    }

    public void StartDialogue(DialogueData.Character character)
    {
        EnqueueDialogues(character);

        if (sentences.Count > 0)
            DisplayCurrentSentence();
        else
        {
            //добавляем предложения только из первого, незавершенного диалога
            foreach (var sentence in dialoguesQueue.Peek().sentences)
            {
                sentences.Enqueue(sentence);
            }

            DisplayCurrentSentence();
        }

        Debug.Log("Dialogue started");
        animator.SetBool(isOpen, true);;
        name.text = dialoguesQueue.Peek().name;
    }
    


    public void DisplayNextSentence()
    {
        var currentDialogue =  dialoguesQueue.Peek();
        if (allDialogues!=null)
        {
            sentences.Dequeue();
            if (sentences.Count == 0)
            {
                TakeQuest(currentDialogue);
                EndDialogue();
                return;
            }
            string sentence = sentences.Peek();
            StopAllCoroutines();
            StartCoroutine(FadeSentence(sentence));
        }
    }
    public void TakeQuest(DialogueData currentDialogue)
    {
        if (!currentDialogue.quest.questTaken)
        {
            LocalQuestsManager.Instance.AddQuest(dialoguesQueue.Peek().quest);
            currentDialogue.quest.questTaken = true;
        }
        
    }
    public void DisplayCurrentSentence()
    {
        string sentence = sentences.Peek();
        StopAllCoroutines();
        StartCoroutine(FadeSentence(sentence));
    }

    public IEnumerator FadeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.04f);
        }
    }

    public void EndDialogue()
    {
        animator.SetBool(isOpen, false);
        StopAllCoroutines();
        dialoguesQueue.Clear();
        sentences.Clear();
    }

}

