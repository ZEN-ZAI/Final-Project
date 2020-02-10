using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSequenceAsset : MonoBehaviour
{
    #region Singleton
    public static DialogueSequenceAsset instance;

    void Awake()
    {
        currentParagraph = -1;

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    [SerializeField] private DialogueParagraph dialogueParagraph;
    public int currentParagraph;
    public int CountParagraph
    {
        get { return dialogueParagraph.dialogueScripts.Count-1; }
    }

    public void SetDialogueParagraph(DialogueParagraph dialogueParagraph)
    {
        this.dialogueParagraph = dialogueParagraph;
        currentParagraph = -1;
    }

    public Dialogue GetDialogueInParagraph(int index)
    {
        return dialogueParagraph.dialogueScripts[index].dialogue;
    }

    public float GetStartDelayParagraph(int index)
    {
        return dialogueParagraph.dialogueScripts[index].startDelay;
    }
}
