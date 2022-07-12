using System;
using UnityEngine;
[CreateAssetMenu(fileName = "NewTutorial", menuName = "TutorialDialogue")]

[Serializable]
public class TutorialDialogue: ScriptableObject
{
    public string Name = "StoryTeller";
    public string Description;
}