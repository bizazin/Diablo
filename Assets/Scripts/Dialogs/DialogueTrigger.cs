using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueData.Character character;
    
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
        FindObjectOfType<DialogueManager>().StartDialogue(character);
    }
    
    private void ExitDialogue()
    {
        FindObjectOfType<DialogueManager>().EndDialogue();
    }

}
