using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    #region Singleton
    public static LoadingScreen instance;

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

    public GameObject loadingScreen;
    public Slider loadingBar;

    public void SetLoadingBar(float progress)
    {
        loadingBar.value = progress;
    }

    public void SetActive(bool state)
    {
        //DebugControl.instance.SetActive(state);
        loadingScreen.SetActive(state);
    }
}