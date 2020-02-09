using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase.Database;

public class SetCompanyName : MonoBehaviour
{
    public GameObject setCompanyNameCanvas;

    public TMP_InputField companyNameField;

    public Button checkButton;
    public Button confirmButton;

    // Start is called before the first frame update
    void Start()
    {
        checkButton.onClick.AddListener(() => CheckCompanyName());

        confirmButton.onClick.AddListener(() =>
        {
            confirmButton.gameObject.SetActive(false);
            Confirm();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (confirmButton.gameObject.activeSelf &&
            companyNameField.text != CompanyStructure.instance.GetCompanyName())
        {
            confirmButton.gameObject.SetActive(false);
        }
    }

    public void SetActive(bool state)
    {
        setCompanyNameCanvas.SetActive(state);
    }

    public bool GetActive()
    {
        return setCompanyNameCanvas.activeSelf;
    }

    private void Confirm()
    {

        UnityMainThreadDispatcher.instance.Enqueue(()=>
        FirebaseDatabase.DefaultInstance.RootReference.Child("Game System/Share Data/Company-UID").OrderByKey().EqualTo(companyNameField.text).GetValueAsync().ContinueWith(task =>
        {
            DataSnapshot snapshot = task.Result;
            string json = snapshot.GetRawJsonValue();

            if (json == "{}" || json == null)
            {
                Dictionary<string, System.Object> childUpdates = new Dictionary<string, System.Object>();
                childUpdates.Add(companyNameField.text, FirebaseData.instance.auth.CurrentUser.UserId);

                UnityMainThreadDispatcher.instance.Enqueue(() => FirebaseDatabase.DefaultInstance.RootReference.Child("Game System/Share Data/Company-UID/").UpdateChildrenAsync(childUpdates));
                UnityMainThreadDispatcher.instance.Enqueue(() => FirebaseData.instance.UpdateCompanyToFirebase());
                UnityMainThreadDispatcher.instance.Enqueue(() => SceneManager.LoadScene("home"));
            }
            else
            {
                UnityMainThreadDispatcher.instance.Enqueue(() => MessageSystem.instance.UpdateMessage("Error, Company name [" + companyNameField.text + "] has exist."));
                UnityMainThreadDispatcher.instance.Enqueue(() => confirmButton.gameObject.SetActive(false));
            }

        }));

    }

    private void CheckCompanyName()
    {
        Debug.Log("CheckCompanyName");

        if (companyNameField.text != "")
        {
            FirebaseDatabase.DefaultInstance.RootReference.Child("Game System/Share Data/Company-UID").OrderByKey().EqualTo(companyNameField.text).GetValueAsync().ContinueWith(task =>
            {
                DataSnapshot snapshot = task.Result;
                string json = snapshot.GetRawJsonValue();

                if (json == "{}" || json == null)
                {
                    Debug.Log("Suscess, You can use " + companyNameField.text);
                    UnityMainThreadDispatcher.instance.Enqueue(() => CompanyStructure.instance.SetCompanyName(companyNameField.text));
                    UnityMainThreadDispatcher.instance.Enqueue(() => MessageSystem.instance.UpdateMessage("Suscess, You can use [" + companyNameField.text+"]"));
                    UnityMainThreadDispatcher.instance.Enqueue(() => confirmButton.gameObject.SetActive(true));
                }
                else
                {
                    UnityMainThreadDispatcher.instance.Enqueue(() => MessageSystem.instance.UpdateMessage("Error, Company name [" + companyNameField.text + "] has exist."));
                }
            });
        }
        else
        {
            MessageSystem.instance.UpdateMessage("Error, Please enter your company name.");
        }

    }

    private IEnumerator Delay(float secound, Action action)
    {
        yield return new WaitForSeconds(secound);
        action();
    }
}
