using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Script", menuName = "Software House Dialogue/Dialogue Script")]
public class DialogueScript : ScriptableObject
{
    public bool playing;
    public float startDelay;
    public Dialogue dialogue;

    private void Awake()
    {
        playing = false;
    }
}
