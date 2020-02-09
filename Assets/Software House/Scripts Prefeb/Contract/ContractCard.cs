using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContractCard : MonoBehaviour
{
    public Transform componentPanel;

    public TMP_Text workID;
    public TMP_Text projectScale;
    public TMP_Text projectType;
    public TMP_Text pay;
    public TMP_Text fine;
    public TMP_Text dueDate;

    public Image rank;

    public Button getButton;

    public Transform content;
    public GameObject componentRequirementPrefab;

    [SerializeField] private ContractData contractData;

    private void Start()
    {
        if (contractData.contractStatus == ContractStatus.Obtain)
        {
            getButton.interactable = false;
            getButton.image.color = Color.gray;
        }

        getButton.onClick.AddListener(() => Get());
    }

    public void SetCardUI(ContractData workData)
    {
        this.contractData = workData;

        workID.text = workData.workID;
        projectScale.text = "Project Scale: "+workData.scaleType.ToString();
        projectType.text = "Project Type: " + workData.contractType.ToString();
        pay.text = workData.reward.money.ToString();
        fine.text = workData.fine.money.ToString();
        dueDate.text = workData.dueDate.week + "/" + workData.dueDate.month + "/" + workData.dueDate.year;

        RemoveAllContent();

        foreach (var item in contractData.genreDatas)
        {
            Instantiate(componentRequirementPrefab, content).GetComponent<ComponentRequirement>().Set(item);
        }

        foreach (var item in contractData.themeDatas)
        {
            Instantiate(componentRequirementPrefab, content).GetComponent<ComponentRequirement>().Set(item);
        }

        foreach (var item in contractData.cameraDatas)
        {
            Instantiate(componentRequirementPrefab, content).GetComponent<ComponentRequirement>().Set(item);
        }

        foreach (var item in contractData.graphicDatas)
        {
            Instantiate(componentRequirementPrefab, content).GetComponent<ComponentRequirement>().Set(item);
        }

        foreach (var item in contractData.platformDatas)
        {
            Instantiate(componentRequirementPrefab, content).GetComponent<ComponentRequirement>().Set(item);
        }

        foreach (var item in contractData.featureDatas)
        {
            Instantiate(componentRequirementPrefab, content).GetComponent<ComponentRequirement>().Set(item);
        }

    }

    public void Get()
    {
        if (WorkStructure.instance.GetOutSourceWorkCount() < CompanyStructure.instance.GetMaxWork().maxContract)
        {
            MessageSystem.instance.UpdateMessage("Get Contract: " + contractData.workID);

            WorkManager.instance.AddOutSourceWork(contractData.Clone());
            contractData.Obtain();

            getButton.interactable = false;
            getButton.image.color = Color.gray;
        }
        else
        {
            MessageSystem.instance.UpdateMessage("Your contract project is max.");
        }
    }

    private void DestroyCard()
    {
        Destroy(this.gameObject);
    }

    public void RemoveAllContent()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }
}
