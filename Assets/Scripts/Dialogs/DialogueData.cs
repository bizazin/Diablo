using System;
using UnityEngine;
[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogues")]

[Serializable]
public class DialogueData: ScriptableObject
{
    public enum Character{
       Storozh = 0,
       Merchant = 1,
    }
    
    public QuestData quest;
    public Character character;
    public string[] sentences;
    public string name;

}
