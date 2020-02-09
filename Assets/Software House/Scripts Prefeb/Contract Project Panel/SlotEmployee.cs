using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SlotEmployee : MonoBehaviour
{
    public Image employeeIcon;
    public Image background;

    public GameObject employeeNamePanel;
    public TMP_Text employeeName;

    public Button addMemberButton;
    public Button removeMemberButton;
    public Button showCardButton;
    public Button showInformationButton;

    [SerializeField] private EmployeeData employeeData;

    public void Set(EmployeeData employeeData)
    {
        NullMode();

        this.employeeData = employeeData;
        employeeIcon.sprite = CharacterAsset.instance.GetCharacterIcon(employeeData.characterID);
        employeeName.text = employeeData.name;
    }

    public void NullMode()
    {
        addMemberButton.gameObject.SetActive(false);
        showCardButton.gameObject.SetActive(false);
        removeMemberButton.gameObject.SetActive(false);
        employeeNamePanel.gameObject.SetActive(false);
    }

    public void ShowCardMode()
    {
        showCardButton.gameObject.SetActive(true);
        showCardButton.onClick.AddListener(() =>
        {
            PanelControl.instance.OpenInformationEmployee();
            InformationEmployee.instance.SetCardUI(employeeData);
        });
    }

    public void InformationMode()
    {
        employeeNamePanel.gameObject.SetActive(true);
        showInformationButton.gameObject.SetActive(true);
        showInformationButton.onClick.AddListener(() =>
        {
            MyEmployeePanel.instance.Set(employeeData);
        });
    }

    //ContractData
    public void AddMemberMode(ContractData contractData, Action refreshMyOutSourceCard)
    {
        addMemberButton.gameObject.SetActive(true);
        addMemberButton.onClick.AddListener(() =>
        {
            contractData.AddMember(employeeData.employeeID);
            employeeData.AddWork("OutSource:" + contractData.workID);
            
            refreshMyOutSourceCard();

            if (EmployeeListPanel.instance != null)
            {
                EmployeeListPanel.instance.RefreshSlotEmployee();
            }
        });
    }

    public void RemoveMemberMode(ContractData contractData, Action refreshMyOutSourceCard)
    {
        removeMemberButton.gameObject.SetActive(true);
        removeMemberButton.onClick.AddListener(() =>
        {
            employeeData.RemoveWork();
            contractData.RemoveMember(employeeData.employeeID);

            refreshMyOutSourceCard();

            if (EmployeeListPanel.instance != null)
            {
                EmployeeListPanel.instance.RefreshSlotEmployee();
            }
        });
    }

    //Research
    public void AddMemberMode(ComponentData componentData, Action refreshSlotEmployee)
    {
        addMemberButton.gameObject.SetActive(true);
        addMemberButton.onClick.AddListener(() =>
        {
            componentData.AddMember(employeeData.employeeID);
            employeeData.AddWork("Research:"+componentData.componentID);

            refreshSlotEmployee();

            if (EmployeeListPanel.instance != null)
            {
                EmployeeListPanel.instance.RefreshSlotEmployee();
            }
        });
    }

    public void RemoveMemberMode(ComponentData componentData,Action refreshSlotEmployee)
    {
        removeMemberButton.gameObject.SetActive(true);
        removeMemberButton.onClick.AddListener(() =>
        {
            employeeData.RemoveWork();
            componentData.RemoveMember(employeeData.employeeID);

            refreshSlotEmployee();

            if (EmployeeListPanel.instance != null)
            {
                EmployeeListPanel.instance.RefreshSlotEmployee();
            }
        });
    }

    //Product
    public void AddMemberMode(Product product, Action refreshSlotEmployee)
    {
        addMemberButton.gameObject.SetActive(true);
        addMemberButton.onClick.AddListener(() =>
        {
            product.AddMember(employeeData.employeeID);
            employeeData.AddWork("Product:" + product.name);

            refreshSlotEmployee();

            if (EmployeeListPanel.instance != null)
            {
                EmployeeListPanel.instance.RefreshSlotEmployee();
            }
        });
    }

    public void RemoveMemberMode(Product product, Action refreshSlotEmployee)
    {
        removeMemberButton.gameObject.SetActive(true);
        removeMemberButton.onClick.AddListener(() =>
        {
            product.RemoveMember(employeeData.employeeID);
            employeeData.RemoveWork();

            refreshSlotEmployee();

            if (EmployeeListPanel.instance != null)
            {
                EmployeeListPanel.instance.RefreshSlotEmployee();
            }
        });
    }

    public EmployeeData GetEmployeeData()
    {
        return employeeData;
    }
}
