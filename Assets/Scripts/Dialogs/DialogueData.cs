using System;
using UnityEngine;
[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogues")]

[Serializable]
public class DialogueData : ScriptableObject
{
    public QuestData Quest;
    public Character Char;
    public string[] Sentences;
    public string Name;

    public enum Character
    {
       Guard = 0,
       Merchant = 1,
    }
}
