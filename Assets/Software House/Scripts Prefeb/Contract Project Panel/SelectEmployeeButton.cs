using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectEmployeeButton : MonoBehaviour
{
    public Button selectMemberButton;

    public void Set(ContractData contractData, Action callBack_action,int maxSlotEmployee)
    {
        selectMemberButton.onClick.AddListener(() =>
        {
            PanelControl.instance.OpenEmployeeListPanel();
            EmployeeListPanel.instance.Set(contractData, callBack_action, maxSlotEmployee);
        });
    }

    public void Set(ComponentData componentData, Action callBack_action, int maxSlotEmployee)
    {
        selectMemberButton.onClick.AddListener(() =>
        {
            PanelControl.instance.OpenEmployeeListPanel();
            EmployeeListPanel.instance.Set(componentData, callBack_action, maxSlotEmployee);
        });
    }

    public void Set(Product product, Action callBack_action, int maxSlotEmployee)
    {
        selectMemberButton.onClick.AddListener(() =>
        {
            PanelControl.instance.OpenEmployeeListPanel();
            EmployeeListPanel.instance.Set(product, callBack_action, maxSlotEmployee);
        });
    }
}