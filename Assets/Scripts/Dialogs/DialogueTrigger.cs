using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueData.Character Charact;
    
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
        FindObjectOfType<DialogueManager>().StartDialogue(Charact);
    }
    
    private void ExitDialogue()
    {
        FindObjectOfType<DialogueManager>().EndDialogue();
    }

}
