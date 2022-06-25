using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DialogueManager : MonoBehaviour
{
    #region Singleton

    public static DialogueManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
    }

    #endregion
    
    [SerializeField] private Text name;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Animator animator;
    private Queue<string> sentences;
    public QuestData questData;
    
    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void AddQuest()
    {
        EventsManager.NewQuestAdded.Invoke(questData);
    }
    
    public void AddPointQuest()
    {
        EventsManager.QuestProgressIncreased.Invoke(questData);
    }
    

    public void StartDialogue(Dialogue dialogue)
    {
       
        if (sentences.Count>0)
        {
            CurrentSentence();
        }
        else
        {
            foreach (var sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            CurrentSentence();
        }
        
        animator.SetBool("IsOpen", true);
        name.text = dialogue.name;
    }
    public void DisplayNextSentence()
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
        animator.SetBool("IsOpen", false);
        StopAllCoroutines();
    }
}

