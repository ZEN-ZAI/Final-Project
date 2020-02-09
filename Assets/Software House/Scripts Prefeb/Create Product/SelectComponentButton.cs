using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectComponentButton : MonoBehaviour
{
    public Button selectComponentButton;

    public void Set(CategorieType categorieType, ComponentType componentTypeMode, List<ComponentData> componentDatas, Action RefreshComponentCard, int maxComponentContent)
    {
        selectComponentButton.onClick.AddListener(() =>
        {
            PanelControl.instance.OpenComponentListPanel();
            ComponentListPanel.instance.Set(categorieType, componentTypeMode, componentDatas, RefreshComponentCard, maxComponentContent);
        });
    }
}
