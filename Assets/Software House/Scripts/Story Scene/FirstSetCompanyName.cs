using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSetCompanyName : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        LoadingScreen.instance.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueSequence.instance.currentParagraph > DialogueSequence.instance.CountParagraph
            && DialogueSystem.instance.sentancesQueue.Count == 0
            && !DialogueSystem.instance.nextButton.gameObject.activeSelf
            && !FindObjectOfType<SetCompanyName>().GetActive())
        {
            StartCoroutine(Delay(2f, () =>
            {
                FindObjectOfType<SetCompanyName>().SetActive(true);
                DialogueSystem.instance.SetActiveDialogueCanvas(false);
            }));

        }
    }

    IEnumerator Delay(float secound, Action action)
    {
        yield return new WaitForSeconds(secound);
        action();
    }
}