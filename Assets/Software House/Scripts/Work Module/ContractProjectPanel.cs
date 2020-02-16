using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContractProjectPanel : MonoBehaviour
{
    #region Singleton
    public static ContractProjectPanel instance;

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

    public TMP_Text projectName;
    public TMP_Text projectScale;
    public TMP_Text projectType;
    public TMP_Text processPercent;
    public TMP_Text dueDate;
    public TMP_Text pay;
    public TMP_Text exp;
    public TMP_Text fire;

    public Image processBar;

    // Component
    public Transform componentGenreContent;
    public Transform componentThemeContent;
    public Transform componentGraphicContent;
    public Transform componentCameraContent;
    public Transform componentFeatureContent;
    public Transform componentPlatformContent;

    public Transform contentEmployee;

    public GameObject claimPanel;
    public Button claimButton;
    public Button cancelButton;
    public Button pinButton;

    public GameObject slotEmployee;
    public GameObject selectEmployeeButton;

    [SerializeField] private ContractData contractData;

    private void Start()
    {
        claimPanel.SetActive(false);
        claimButton.onClick.AddListener(()=> ClaimReward());
        cancelButton.onClick.AddListener(() => RemoveProject());
    }

    void Update()
    {
        if (contractData != null)
        {
            UpdateProcessUI();

            if (contractData.isDone && !claimPanel.activeSelf)
            {
                RefreshSlotEmployee();
                claimPanel.SetActive(true);
            }
        }
    }

    public void Set(ContractData contractData)
    {
        this.contractData = contractData;
        //pinButton.onClick.AddListener(() => UIControl.instance.workUI.Set(contractData));
        projectName.text = contractData.workID;
        projectScale.text = contractData.scaleType.ToString();
        projectType.text = contractData.contractType.ToString();
        dueDate.text = contractData.dueDate.week + "/"+ contractData.dueDate.month + "/"+ contractData.dueDate.year;

        pay.text = contractData.reward.money.ToString("C0").Replace("$", "");
        exp.text = contractData.reward.exp.ToString("C0").Replace("$", "");
        fire.text = contractData.fine.money.ToString("C0").Replace("$", "");

        processBar.fillAmount = contractData.processCurrent / contractData.processMax;

        UpdateProcessUI();
        RefreshComponentCardUI();
        RefreshSlotEmployee();
    }

    public ContractData GetcontractDataData()
    {
        return contractData;
    }

    private void ClaimReward()
    {
        contractData.ClaimReward();
        Destroy(this.gameObject);
    }

    private void RemoveProject()
    {
        contractData.Fine();
        Destroy(this.gameObject);
    }

    public void UpdateProcessUI()
    {
        if (contractData.processCurrent > 0)
        {
            float workProcess = ((float)contractData.processCurrent / contractData.processMax) * 100;
            processPercent.text = "Process "+(int)workProcess + "%";
            processBar.fillAmount = (float)contractData.processCurrent / contractData.processMax;
        }
        else
        {
            processBar.fillAmount = 0;
            processPercent.text = "Process 0%";
        }
    }

    public void RefreshComponentCardUI()
    {
        AddComponentCard(ComponentType.Genre, componentGenreContent, contractData.genreDatas);
        AddComponentCard(ComponentType.Theme, componentThemeContent, contractData.themeDatas);
        AddComponentCard(ComponentType.Graphic, componentGraphicContent, contractData.graphicDatas);
        AddComponentCard(ComponentType.Camera, componentCameraContent, contractData.cameraDatas);
        AddComponentCard(ComponentType.Feature, componentFeatureContent, contractData.featureDatas);
        AddComponentCard(ComponentType.Platform, componentPlatformContent, contractData.platformDatas);
    }

    public void AddComponentCard(ComponentType componentType, Transform content, List<ComponentData> componentDatas)
    {
        content.GetComponent<ComponentCard>().ProductMode(CategorieType.Game, componentType, componentDatas);
    }

    public void RefreshSlotEmployee()
    {
        RemoveAllSlotEmployee();
        AddSlotEmployee();
    }

    private void AddSlotEmployee()
    {
        List<string> employeeIDs = contractData.employee_Worker;

        for (int i = 0; i < CompanyStructure.instance.GetMaxSlot().maxOutsourceMember; i++)
        {
            if (i < employeeIDs.Count)
            {
                SlotEmployee slotEmployee_temp = Instantiate(slotEmployee, contentEmployee).GetComponent<SlotEmployee>();
                EmployeeData employeeData_temp = EmployeeStructure.instance.GetMyEmployeeData(employeeIDs[i]);
                slotEmployee_temp.Set(employeeData_temp);

                slotEmployee_temp.RemoveMemberMode(contractData, RefreshSlotEmployee);
            }
            else
            {
                Instantiate(selectEmployeeButton, contentEmployee).GetComponent<SelectEmployeeButton>().Set(contractData, ()=>
                {
                    RefreshSlotEmployee();
                }, CompanyStructure.instance.GetMaxSlot().maxOutsourceMember);
            }
        }
    }

    private void RemoveAllSlotEmployee()
    {
        for (int i = 0; i < contentEmployee.childCount; i++)
        {
            Destroy(contentEmployee.GetChild(i).gameObject);
        }
    }
}
