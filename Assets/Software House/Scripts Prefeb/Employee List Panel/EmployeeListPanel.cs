using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EmployeeListPanel : MonoBehaviour
{
    #region Singleton
    public static EmployeeListPanel instance;

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
    }
    #endregion

    public Transform content;
    public GameObject slotEmployee;

    [SerializeField] private ComponentData componentData;
    [SerializeField] private ContractData contractData;
    [SerializeField] private Product product;

    [SerializeField] private Action refresh;

    [SerializeField] private int maxEmployeeContent;

    public bool outsourceMode;
    public bool researchMode;
    public bool productMode;

    public void OutsourceMode()
    {
        print("Outsource Mode");
        outsourceMode = true;
        researchMode = false;
        productMode = false;
    }

    public void ResearchMode()
    {
        print("Research Mode");
        outsourceMode = false;
        researchMode = true;
        productMode = false;
    }

    public void ProductMode()
    {
        print("Product Mode");
        outsourceMode = false;
        researchMode = false;
        productMode = true;
    }

    public void Set(ContractData contractData, Action refresh, int maxEmployeeContent)
    {
        OutsourceMode();

        this.contractData = contractData;
        this.componentData = null;
        this.product = null;

        this.refresh = refresh;
        this.maxEmployeeContent = maxEmployeeContent;

        RefreshSlotEmployee();
    }

    public void Set(ComponentData componentData, Action refresh, int maxEmployeeContent)
    {
        ResearchMode();

        this.componentData = componentData;
        this.contractData = null;
        this.product = null;

        this.refresh = refresh;
        this.maxEmployeeContent = maxEmployeeContent;

        RefreshSlotEmployee();
    }

    public void Set(Product product, Action refresh, int maxEmployeeContent)
    {
        ProductMode();

        this.product = product;
        this.contractData = null;

        this.refresh = refresh;
        this.maxEmployeeContent = maxEmployeeContent;

        RefreshSlotEmployee();
    }


    public void RefreshSlotEmployee()
    {
        RemoveAllSlotEmployee();
        AddSlotEmployee();
    }

    private void AddSlotEmployee()
    {
        List<EmployeeData> employeeDatas = EmployeeStructure.instance.GetMyEmployeeDataList();

        for (int i = 0; i < employeeDatas.Count; i++)
        {
            EmployeeData employeeData_temp = employeeDatas[i];
            SlotEmployee SlotEmployee_temp = Instantiate(slotEmployee, content).GetComponent<SlotEmployee>();
            SlotEmployee_temp.Set(employeeData_temp);
            SlotEmployee_temp.ShowCardMode();

            if (productMode)
            {
                if (!employeeData_temp.haveWork && product.employee_Worker.Count < maxEmployeeContent)
                {
                    SlotEmployee_temp.AddMemberMode(product, refresh);
                }
            }
            else if (outsourceMode)
            {
                if (!employeeData_temp.haveWork && contractData.employee_Worker.Count < maxEmployeeContent)
                {
                    SlotEmployee_temp.AddMemberMode(contractData, refresh);
                }
            }
            else if (researchMode)
            {
                if (!employeeData_temp.haveWork && componentData.employee_Worker.Count < maxEmployeeContent)
                {
                    SlotEmployee_temp.AddMemberMode(componentData, refresh);
                }
            }
            
        }
    }

    private void RemoveAllSlotEmployee()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }
}