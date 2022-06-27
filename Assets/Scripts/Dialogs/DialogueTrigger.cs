using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    
    private void OnTriggerEnter(Collider other)
    {
        TriggerDialogue();
    }
    
    private void OnTriggerExit(Collider other)
    {
        ExitDialogue();
    }
    
    private void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    
    private void ExitDialogue()
    {
        FindObjectOfType<DialogueManager>().EndDialogue();
    }

}
