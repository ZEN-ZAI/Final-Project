using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FindContractPanel : MonoBehaviour
{
    #region Singleton
    public static FindContractPanel instance;

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

    public Transform selectOutsourceTypePanel;
    public Transform contractCardPanel;

    public Button scenarioType;
    public Button graphicType;
    public Button moduleType;
    public Button supportType;
    public Button fullGameProjectType;

    public Transform content;
    public GameObject contractOutsoruceCard;

    void Start()
    {
        selectOutsourceTypePanel.gameObject.SetActive(true);
        contractCardPanel.gameObject.SetActive(false);

        scenarioType.onClick.AddListener(() =>
        {
            WorkManager.instance.RandomOutsourceScenarioType(5);
            RefreshContractOutsoruceCardUI(WorkStructure.instance.GetContractScenarioTypeList());

            selectOutsourceTypePanel.gameObject.SetActive(false);
            contractCardPanel.gameObject.SetActive(true);
        });

        graphicType.onClick.AddListener(() =>
        {
            WorkManager.instance.RandomOutsourceGraphicType(5);
            RefreshContractOutsoruceCardUI(WorkStructure.instance.GetContractGraphicTypeList());

            selectOutsourceTypePanel.gameObject.SetActive(false);
            contractCardPanel.gameObject.SetActive(true);
        });

        moduleType.onClick.AddListener(() =>
        {
            WorkManager.instance.RandomOutsourceModuleType(5);
            RefreshContractOutsoruceCardUI(WorkStructure.instance.GetContractModuleTypeList());

            selectOutsourceTypePanel.gameObject.SetActive(false);
            contractCardPanel.gameObject.SetActive(true);
        });

        supportType.onClick.AddListener(() =>
        {
            WorkManager.instance.RandomOutsourceSupportType(5);
            RefreshContractOutsoruceCardUI(WorkStructure.instance.GetContractSupportTypeList());

            selectOutsourceTypePanel.gameObject.SetActive(false);
            contractCardPanel.gameObject.SetActive(true);
        });

        fullGameProjectType.onClick.AddListener(() =>
        {
            WorkManager.instance.RandomOutsourceFullGameProjectType(5);
            RefreshContractOutsoruceCardUI(WorkStructure.instance.GetContractFullGameProjectTypeList());

            selectOutsourceTypePanel.gameObject.SetActive(false);
            contractCardPanel.gameObject.SetActive(true);
        });
    }

    public void RefreshContractOutsoruceCardUI(List<ContractData> contractDatas)
    {
        RemoveAllContractOutsoruceCardUI();
        AddContractOutsoruceCardUI(contractDatas);
    }

    public void RemoveAllContractOutsoruceCardUI()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    public void AddContractOutsoruceCardUI(List<ContractData> contractDatas)
    {
        foreach (ContractData contractData in contractDatas)
        {
            GameObject temp_ContractOutsoruceCard = Instantiate(contractOutsoruceCard, content);
            temp_ContractOutsoruceCard.GetComponent<ContractCard>().SetCardUI(contractData);
        }
    }
}
