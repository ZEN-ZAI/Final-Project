using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchPanel : MonoBehaviour
{
    #region Singleton
    public static ResearchPanel instance;

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

    public Button genreButton;
    public Button themeButton;
    public Button graphicButton;
    public Button cameraButton;
    public Button featureButton;
    public Button platformButton;

    public Button refreshAllButton;

    public Transform memberPanel;
    public Transform memberContent;

    public Transform componentGenrePanel;
    public Transform componentThemePanel;
    public Transform componentGraphicPanel;
    public Transform componentCameraPanel;
    public Transform componentFeaturePanel;
    public Transform componentPlatformPanel;

    public Transform componentGenreContent;
    public Transform componentThemeContent;
    public Transform componentGraphicContent;
    public Transform componentCameraContent;
    public Transform componentFeatureContent;
    public Transform componentPlatformContent;

    public GameObject slotComponent;
    public GameObject slotEmployee;
    public GameObject selectEmployeeButton;

    private void Start()
    {
        CloseAllPanel();
        componentGenrePanel.gameObject.SetActive(true);
        RefreshSlotComponentGenre();

        refreshAllButton.onClick.AddListener(() =>
        {
            CloseAllPanel();
            RefreshAllComponent();
        });

        genreButton.onClick.AddListener(() =>
        {
            CloseAllPanel();
            componentGenrePanel.gameObject.SetActive(true);
            RefreshSlotComponentGenre();
        });

        themeButton.onClick.AddListener(() =>
        {
            CloseAllPanel();
            componentThemePanel.gameObject.SetActive(true);
            RefreshSlotComponentTheme();
        });

        graphicButton.onClick.AddListener(() =>
        {
            CloseAllPanel();
            componentGraphicPanel.gameObject.SetActive(true);
            RefreshSlotComponentGraphic();
        });

        cameraButton.onClick.AddListener(() =>
        {
            CloseAllPanel();
            componentCameraPanel.gameObject.SetActive(true);
            RefreshSlotComponentCamera();
        });

        featureButton.onClick.AddListener(() =>
        {
            CloseAllPanel();
            componentFeaturePanel.gameObject.SetActive(true);
            RefreshSlotComponentFeature();
        });

        platformButton.onClick.AddListener(() =>
        {
            CloseAllPanel();
            componentPlatformPanel.gameObject.SetActive(true);
            RefreshSlotComponentPlatform();
        });
    }

    public void RefreshAllComponent()
    {
        RefreshSlotComponentGenre();
        RefreshSlotComponentTheme();
        RefreshSlotComponentGraphic();
        RefreshSlotComponentCamera();
        RefreshSlotComponentFeature();
        RefreshSlotComponentPlatform();
    }

    private void CloseAllPanel()
    {
        componentGenrePanel.gameObject.SetActive(false);
        componentThemePanel.gameObject.SetActive(false);
        componentGraphicPanel.gameObject.SetActive(false);
        componentCameraPanel.gameObject.SetActive(false);
        componentFeaturePanel.gameObject.SetActive(false);
        componentPlatformPanel.gameObject.SetActive(false);
        memberPanel.gameObject.SetActive(false);
    }

    public void RefreshSlotComponentGenre()
    {
        RemoveAllSlotComponent(componentGenreContent);
        AddSlotComponent(StartupStructure.instance.GetGenreResearchList(), componentGenreContent);
    }

    public void RefreshSlotComponentTheme()
    {
        RemoveAllSlotComponent(componentThemeContent);
        AddSlotComponent(StartupStructure.instance.GetThemeResearchLst(), componentThemeContent);
    }

    public void RefreshSlotComponentGraphic()
    {
        RemoveAllSlotComponent(componentGraphicContent);
        AddSlotComponent(StartupStructure.instance.GetGraphicResearchList(), componentGraphicContent);
    }

    public void RefreshSlotComponentCamera()
    {
        RemoveAllSlotComponent(componentCameraContent);
        AddSlotComponent(StartupStructure.instance.GetCameraResearchList(), componentCameraContent);
    }

    public void RefreshSlotComponentFeature()
    {
        RemoveAllSlotComponent(componentFeatureContent);
        AddSlotComponent(StartupStructure.instance.GetFeatureResearchList(), componentFeatureContent);
    }

    public void RefreshSlotComponentPlatform()
    {
        RemoveAllSlotComponent(componentPlatformContent);
        AddSlotComponent(StartupStructure.instance.GetPlatformResearchList(), componentPlatformContent);
    }

    private void AddSlotComponent(List<ComponentData> componentData_list, Transform content)
    {
        foreach (ComponentData componentData in componentData_list)
        {
            SlotComponent slotComponent_temp = Instantiate(slotComponent, content).GetComponent<SlotComponent>();
            slotComponent_temp.ResearchMode(componentData,
                () =>
                {
                    memberPanel.gameObject.SetActive(true);
                    RefreshSlotEmployee(componentData);
                });
        }
    }

    private void RemoveAllSlotComponent(Transform content)
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    public void RefreshSlotEmployee(ComponentData componentData)
    {
        RemoveAllSlotEmployee();
        AddSlotEmployee(componentData);
        memberPanel.gameObject.SetActive(true);
    }

    private void AddSlotEmployee(ComponentData componentData)
    {
        List<string> employeeIDs = componentData.employee_Worker;

        for (int i = 0; i < CompanyStructure.instance.GetMaxSlot().maxResearchMember; i++)
        {
            if (i < employeeIDs.Count)
            {
                SlotEmployee slotEmployee_temp = Instantiate(slotEmployee, memberContent).GetComponent<SlotEmployee>();
                EmployeeData employeeData_temp = EmployeeStructure.instance.GetMyEmployeeData(employeeIDs[i]);
                slotEmployee_temp.Set(employeeData_temp);

                slotEmployee_temp.RemoveMemberMode(componentData, ()=> RefreshSlotEmployee(componentData));
            }
            else
            {
                Instantiate(selectEmployeeButton, memberContent).GetComponent<SelectEmployeeButton>().Set(componentData, () => RefreshSlotEmployee(componentData), CompanyStructure.instance.GetMaxSlot().maxResearchMember);
            }
        }
    }

    private void RemoveAllSlotEmployee()
    {
        for (int i = 0; i < memberContent.childCount; i++)
        {
            Destroy(memberContent.GetChild(i).gameObject);
        }
    }
}


