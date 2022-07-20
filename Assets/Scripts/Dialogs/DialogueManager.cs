using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
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
    private string isOpen;
    private float charDelay;
        
    public QuestData QuestData;
    

    private void Start()
    {
        isOpen = "IsOpen";
        sentences = new Queue<string>();
        dialoguesQueue = new Queue<DialogueData>();
    }

    public void EnqueueDialogues(DialogueData.Character character)
    {
        foreach (var dialogue in allDialogues)
            if (dialogue.Char == character && !dialogue.Quest.Completed)
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
            foreach (var sentence in dialoguesQueue.Peek().Sentences)
                sentences.Enqueue(sentence);

            DisplayCurrentSentence();
        }

        Debug.Log("Dialogue started");
        animator.SetBool(isOpen, true); ;
        name.text = dialoguesQueue.Peek().Name;
    }

    public void DisplayNextSentence()
    {
        var currentDialogue = dialoguesQueue.Peek();
        if (allDialogues != null)
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
        if (!currentDialogue.Quest.QuestTaken)
        {
            LocalQuestsManager.Instance.AddQuest(dialoguesQueue.Peek().Quest);
            currentDialogue.Quest.QuestTaken = true;
        }

        switch (currentDialogue.Char)
        {
            case DialogueData.Character.Guard:
                int idMainQuest = 1;
                EventsManager.MainQuestProgressIncreased?.Invoke(idMainQuest);
                break;
            case DialogueData.Character.Merchant:
                int idLocalQuest = 2;
                EventsManager.LocalQuestProgressIncreased?.Invoke(idLocalQuest);
                break;
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
        dialogueText.text = string.Empty;

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(charDelay);
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

