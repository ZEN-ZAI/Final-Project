using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInformation : MonoBehaviour
{
    #region Singleton
    public static ShopInformation instance;
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

    public void UpdateInformation()
    {

    }
}
