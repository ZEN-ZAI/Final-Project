using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SlotComponent : MonoBehaviour
{
    public Image componentIcon;
    public Image background;

    public GameObject componentNamePanel;
    public TMP_Text componentName;

    public Transform processPanel;
    public TMP_Text ProgressIndicator;
    public Image LoadingBar;

    public Button addComponentButton;
    public Button removeComponentButton;
    public Button showMemberButton; // for research
    public Button informationButton;

    [SerializeField] private bool displayOnlyMode;
    [SerializeField] private bool processMode;
    [SerializeField] private bool createProductMode;
    [SerializeField] private bool researchMode;
    [SerializeField] private bool componentListAddMode;

    [SerializeField] private ComponentData componentData;

    private void Awake()
    {
        NullMode();
    }

    public void NullMode()
    {
        processPanel.gameObject.SetActive(false);
        showMemberButton.gameObject.SetActive(false);
        addComponentButton.gameObject.SetActive(false);
        removeComponentButton.gameObject.SetActive(false);
        informationButton.gameObject.SetActive(false);
        componentNamePanel.gameObject.SetActive(false);
    }

    public void DisplayOnlyMode(ComponentData componentData)
    {
        displayOnlyMode = true;

        Display(componentData);
    }

    public void ResearchMode(ComponentData componentData, Action action)
    {
        researchMode = true;

        Display(componentData);
        InformaionResearchMode();
        ShowMemberMode(action);
    }

    public void ComponentListAddMode(ComponentData componentData, List<ComponentData> componentDatas, Action action)
    {
        componentListAddMode = true;
        Display(componentData);
        AddComponentMode(componentDatas, action);
    }

    public void CreateProductMode(ComponentData componentData, List<ComponentData> componentDatas, Action action)
    {
        createProductMode = true;
        Display(componentData);
        InformaionProductMode();
        RemoveComponentMode(componentDatas, action);
    }

    public void ProcessMode(ComponentData componentData)
    {
        processMode = true;
        processPanel.gameObject.SetActive(true);

        Display(componentData);
        InformaionProductMode();
    }

    private void Display(ComponentData componentData)
    {
        this.componentData = componentData;

        Component component = ComponentAsset.instance.GetComponentAsset(componentData.componentID);

        componentNamePanel.gameObject.SetActive(true);
        componentIcon.sprite = component.icon;
        componentName.text = component.name.Replace("_", "");
    }


    private void Update()
    {
        if (processMode)
        {
            Process();
        }
    }

    private void Process()
    {

        if (componentData.developLevel == 0)
        {
            ProgressIndicator.text = "";
            LoadingBar.fillAmount = 0;
            componentIcon.color = Color.gray;
            return;
        }

        if (componentData.developLevel > 0 && componentData.developLevel < componentData.productLevel)
        {
            float process = ((float)componentData.developLevel / componentData.productLevel) * 100;

            print("process "+componentData.componentID+" "+ process + "% ("+ componentData.developLevel+"/"+ componentData.productLevel);

            ProgressIndicator.text = (int)process + "%";
            LoadingBar.fillAmount = (float)componentData.developLevel / componentData.productLevel;
            componentIcon.color = Color.gray;
        }
        else if (componentData.developLevel == componentData.productLevel)
        {
            processPanel.gameObject.SetActive(false);
            ProgressIndicator.text = "";
            LoadingBar.fillAmount = 0;
            componentIcon.color = Color.white;
        }
        
    }

    private void InformaionProductMode()
    {
        informationButton.gameObject.SetActive(true);
        informationButton.onClick.AddListener(() =>
        {
            PanelControl.instance.OpenInformationComponent();
            InformationComponent.instance.SetProductMode(componentData);
        });
    }

    private void InformaionResearchMode()
    {
        informationButton.gameObject.SetActive(true);
        informationButton.onClick.AddListener(() =>
        {
            PanelControl.instance.OpenInformationComponent();
            InformationComponent.instance.SetResearchMode(componentData);
        });
    }

    private void AddComponentMode(List<ComponentData> componentDatas, Action RefreshComponentCard)
    {
        addComponentButton.gameObject.SetActive(true);
        addComponentButton.onClick.AddListener(() =>
        {
            componentData.productLevel = 10000;
            componentData.developLevel = componentData.researchLevel;
            componentDatas.Add(componentData);
            RefreshComponentCard();

            if (ComponentListPanel.instance != null)
            {
                ComponentListPanel.instance.RefreshSlotComponent();
            }
        });
    }

    private void RemoveComponentMode(List<ComponentData> componentDatas, Action refreshMyOutSourceCard)
    {
        removeComponentButton.gameObject.SetActive(true);
        removeComponentButton.onClick.AddListener(() =>
        {
            componentDatas.Remove(componentData);
            refreshMyOutSourceCard();

            if (ComponentListPanel.instance != null)
            {
                ComponentListPanel.instance.RefreshSlotComponent();
            }

            if (InformationComponent.instance != null)
            {
                Destroy(InformationComponent.instance);
            }
        });
    }

    private void ShowMemberMode(Action showMemberPanel)
    {
        showMemberButton.gameObject.SetActive(true);
        showMemberButton.onClick.AddListener(() =>
        {
            showMemberPanel();
        });
    }

    private ComponentData GetComponentData()
    {
        return componentData;
    }
}