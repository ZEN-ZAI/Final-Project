using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Paragraph", menuName = "Software House Dialogue/Dialogue Paragraph")]
public class DialogueParagraph : ScriptableObject
{
    public List<DialogueScript> dialogueScripts;
}