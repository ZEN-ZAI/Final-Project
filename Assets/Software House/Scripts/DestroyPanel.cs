using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DestroyPanel : MonoBehaviour
{
    public GameObject rootPanel;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Destroy(rootPanel.gameObject);

            if (rootPanel.GetComponent<ComponentListPanel>() == null || rootPanel.GetComponent<EmployeeListPanel>() == null)
            {
                CameraPanZoom.instance.SetActive(true);
            }
        });

        if (rootPanel.GetComponent<ShopPanel>() != null)
        {
            GetComponent<Button>().onClick.AddListener(() =>
               MapManager.instance.MapEditorSetActive(false));
        }
    }

    public void Invoke()
    {
        GetComponent<Button>().onClick.Invoke();
    }
}