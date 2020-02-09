using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageSystem : MonoBehaviour
{
    #region Singleton
    public static MessageSystem instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    private void Start()
    {
        ClosePanelWarning();
    }

    public GameObject canvas;
    public TMP_Text textMessage;

    private void Update()
    {
        textMessage.ForceMeshUpdate(true);
    }

    public void UpdateMessage(string message)
    {
        print("MessageSystem: "+message);
        textMessage.text = message;
        OpenPanelWarning();

        Invoke("ClosePanelWarning", 3f);
    }

    public void ClosePanelWarning()
    {
        canvas.SetActive(false);
    }

    public void OpenPanelWarning()
    {
        canvas.SetActive(true);
    }
}
