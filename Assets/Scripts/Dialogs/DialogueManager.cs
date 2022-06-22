using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Text name;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Animator animator;
    private Queue<string> sentences;
    private void Start()
    {
        sentences = new Queue<string>();
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

