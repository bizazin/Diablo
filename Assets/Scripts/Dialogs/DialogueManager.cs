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
    [SerializeField] private List<DialogueData> currentDialogues;
    
    private string isOpen = "IsOpen";
    private Queue<string> sentences;
    public QuestData questData;
    
    
    private void Start()
    {
        sentences = new Queue<string>();
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
                currentDialogues.Add(dialogue);
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
            foreach (var sentence in currentDialogues[0].sentences)
            {
                sentences.Enqueue(sentence);
            }
            CurrentSentence();
        }
        Debug.Log("Dialogue started" );
        animator.SetBool(isOpen, true);
        //name.text = allDialogues.name;
    }
    public void DisplayNextSentence()
    {
        if (allDialogues!=null)
        {
            sentences.Dequeue();
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            string sentence = sentences.Peek();
            StopAllCoroutines();
            StartCoroutine(FadeSentence(sentence));
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

