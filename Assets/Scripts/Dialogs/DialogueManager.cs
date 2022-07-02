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
    [SerializeField] private Queue<DialogueData> currentDialogues;
    
    private string isOpen = "IsOpen";
    private Queue<string> sentences;
    public QuestData questData;
    
    
    private void Start()
    {
        sentences = new Queue<string>();
        currentDialogues = new Queue<DialogueData>();
    }

    public void AddQuest()
    {
        LocalQuestsManager.Instance.AddQuest(questData);
    }
    
    public void AddPointQuest()
    {
        EventsManager.LocalQuestProgressIncreased.Invoke(questData);
    }

    public void SortDialogues(DialogueData.Character character)
    {
        foreach (var dialogue in allDialogues)
            if(dialogue.character == character && !dialogue.quest.completed)
                currentDialogues.Enqueue(dialogue);
    }

    public void StartDialogue(DialogueData.Character character)
    {
       // allDialogues = dialogue;
        SortDialogues(character);
       
        if (sentences.Count>0)
        {
            CurrentSentence();
        }
        else
        {
            //добавляем предложения только из первого, незавершенного диалога
            foreach (var sentence in currentDialogues.Peek().sentences)
            {
                sentences.Enqueue(sentence);
            }
            CurrentSentence();
        }
        Debug.Log("Dialogue started" );
        animator.SetBool(isOpen, true);
        name.text = currentDialogues.Peek().name;
    }
    public void DisplayNextSentence()
    {
        var currentDialogue =  currentDialogues.Peek();
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
            LocalQuestsManager.Instance.AddQuest(currentDialogues.Peek().quest);
            currentDialogue.quest.questTaken = true;
        }
        
    }
    public void CurrentSentence()
    {
        string sentence = sentences.Peek();
        StopAllCoroutines();
        StartCoroutine(FadeSentence(sentence));
    }
    IEnumerator FadeSentence(string sentence)
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
        currentDialogues.Clear();
        sentences.Clear();
    }

}

