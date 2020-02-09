using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{
    #region Singleton
    public static PanelControl instance;

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

    public Button menu;
    public Transform menuPanel;

    public Button myEmployeeButton;
    public Button shopButton;
    public Button contractListButton;
    public Button recruitmentButton;
    public Button contractOutsourceButton;
    public Button researchButton;
    public Button productListButton;

    public GameObject myEmployeePanel;
    public GameObject shopPanel;
    public GameObject createProductPanel;
    public GameObject contractProjectPanel;
    public GameObject productPanel;
    public GameObject recruitmentPanel;
    public GameObject FindContractPanel;
    public GameObject researchPanel;

    public GameObject employeeListPanel;
    public GameObject componentListPanel;
    public GameObject productListPanel;
    public GameObject contractListPanel;

    public GameObject informationEmployee;
    public GameObject informationComponent;

    private void Start()
    {
        menu.onClick.AddListener(() =>
        {
            if (!menuPanel.gameObject.activeSelf)
            {
                MenuSetActive(true);
            }
            else
            {
                MenuSetActive(false);
            }
        });

        myEmployeeButton.onClick.AddListener(() => OpenMyEmployeePanel());
        shopButton.onClick.AddListener(() => OpenShopPanel());
        contractListButton.onClick.AddListener(() => OpenContractListPanel());
        recruitmentButton.onClick.AddListener(() => OpenRecruitmentPanel());
        contractOutsourceButton.onClick.AddListener(() => OpenFindContractOutsourcePanel());
        researchButton.onClick.AddListener(() => OpenResearchPanel());
        productListButton.onClick.AddListener(() => OpenProductListPanel());
    }

    public void MenuSetActive(bool state)
    {
        menuPanel.gameObject.SetActive(state);

        if (state)
        {
            menu.image.color = Color.gray;
        }
        else
        {
            menu.image.color = Color.white;
        }
    }

    public void OpenMyEmployeePanel()
    {
        CameraPanZoom.instance.SetActive(false);
        Instantiate(myEmployeePanel);
    }

    public void OpenCreateProductPanel()
    {
        CameraPanZoom.instance.SetActive(false);
        Instantiate(createProductPanel);
    }

    public void OpenContractProjectPanel()
    {
        CameraPanZoom.instance.SetActive(false);
        Instantiate(contractProjectPanel);
    }

    public void OpenProductDevelopmentPanel()
    {
        CameraPanZoom.instance.SetActive(false);
        Instantiate(productPanel);
    }

    public void OpenRecruitmentPanel()
    {
        CameraPanZoom.instance.SetActive(false);
        Instantiate(recruitmentPanel);
    }

    public void OpenFindContractOutsourcePanel()
    {
        CameraPanZoom.instance.SetActive(false);
        Instantiate(FindContractPanel);
    }

    public void OpenResearchPanel()
    {
        CameraPanZoom.instance.SetActive(false);
        Instantiate(researchPanel);
    }

    public void OpenShopPanel()
    {
        MapManager.instance.MapEditorSetActive(true);
        MenuSetActive(false);
        Instantiate(shopPanel);
    }

    public void OpenInformationEmployee()
    {
        Instantiate(informationEmployee);
    }

    public void OpenInformationComponent()
    {
        if (ComponentListPanel.instance != null)
        {
            Destroy(ComponentListPanel.instance.gameObject);
        }

        Instantiate(informationComponent);
    }
    public void OpenEmployeeListPanel()
    {
        Instantiate(employeeListPanel);
    }

    public void OpenComponentListPanel()
    {
        if (InformationComponent.instance != null)
        {
            Destroy(InformationComponent.instance.gameObject);
        }

        Instantiate(componentListPanel);
    }

    public void OpenProductListPanel()
    {
        CameraPanZoom.instance.SetActive(false);
        Instantiate(productListPanel);
    }

    public void OpenContractListPanel()
    {
        CameraPanZoom.instance.SetActive(false);
        Instantiate(contractListPanel);
    }
}
