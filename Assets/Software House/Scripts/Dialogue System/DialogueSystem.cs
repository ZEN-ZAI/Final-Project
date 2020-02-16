using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueSystem : MonoBehaviour
{
    #region Singleton
    public static DialogueSystem instance;

    void Awake()
    {
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

    public GameObject dialogueCanvas;

    public Image characterImage;
    public TMP_Text characterNameText;
    public TMP_Text dialogueText;
    public Button nextButton;
    public Queue<string> sentancesQueue = new Queue<string>();

    public string tempSentances;
    public bool typesentancesIsEnd;

    private void Start()
    {
        nextButton.onClick.AddListener(() =>
        {
            StopAllCoroutines();

            if (typesentancesIsEnd)
            {
                DisplayNextSentance();
            }
            else
            {
                BypassTypesentances();
            }
        });

        NextParagraph();
    }

    public void SetActiveDialogueCanvas(bool state)
    {
        dialogueCanvas.gameObject.SetActive(state);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentancesQueue.Clear();
        nextButton.gameObject.SetActive(true);

        characterImage.sprite = dialogue.characterSprite;
        characterNameText.text = dialogue.characterName + "";
        Debug.Log("[Dialogue Start] Owner is " + dialogue.characterName);

        foreach (string sentance in dialogue.sentances)
        {
            sentancesQueue.Enqueue(sentance);
        }

        DisplayNextSentance();
    }

    public void DisplayNextSentance()
    {
        string sentance = sentancesQueue.Dequeue();
        Debug.Log(sentance);
        dialogueText.text = sentance + "";

        StartCoroutine(TypesentancesQueue(sentance));
        tempSentances = sentance;

        if (sentancesQueue.Count == 0)
        {
            Debug.Log("[Dialogue End]");
            nextButton.gameObject.SetActive(false);
            NextParagraph();
        }

    }

    IEnumerator TypesentancesQueue(string sentance)
    {
        dialogueText.text = "";
        
        foreach (var letter in sentance.ToCharArray())
        {
            typesentancesIsEnd = false;
            SoundManager.instance.PlayDialogPimp();
            dialogueText.text += letter;
            yield return null;
            typesentancesIsEnd = true;
        }
    }

    public void BypassTypesentances()
    {
        typesentancesIsEnd = true;
        dialogueText.text = tempSentances;
    }

    public void NextParagraph()
    {
        int index = ++DialogueSequenceAsset.instance.currentParagraph;

        if (DialogueSequenceAsset.instance.currentParagraph <= DialogueSequenceAsset.instance.CountParagraph)
        {
            Dialogue dialogue = DialogueSequenceAsset.instance.GetDialogueInParagraph(index);
            StartCoroutine(Delay(DialogueSequenceAsset.instance.GetStartDelayParagraph(index), ()=> StartDialogue(dialogue)));
        }
    }

    IEnumerator Delay(float secound,Action action)
    {
        yield return new WaitForSeconds(secound);
        action();
    }
}
