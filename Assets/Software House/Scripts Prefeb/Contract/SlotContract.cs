using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotContract : MonoBehaviour
{
    public Image contractIcon;
    public Image background;

    public GameObject contractNamePanel;
    public TMP_Text contractName;

    public Button addContractButton;
    public Button showContractButton;

    [SerializeField] private ContractData contractData;

    public void Set(ContractData contractData)
    {
        NullMode();

        this.contractData = contractData;
        contractName.text = contractData.workID;
    }

    public void NullMode()
    {
        contractNamePanel.gameObject.SetActive(false);
        addContractButton.gameObject.SetActive(false);
        showContractButton.gameObject.SetActive(false);
    }

    public void AddContractMode()
    {
        addContractButton.gameObject.SetActive(true);
        addContractButton.onClick.AddListener(() =>
        {
            PanelControl.instance.OpenFindContractOutsourcePanel();
            ContractListPanel.instance.Destroy();
        });
    }

    public void ShowContractMode()
    {
        contractNamePanel.gameObject.SetActive(true);
        showContractButton.gameObject.SetActive(true);
        showContractButton.onClick.AddListener(() =>
        {
            PanelControl.instance.OpenContractProjectPanel();
            ContractProjectPanel.instance.Set(contractData);
            ContractListPanel.instance.Destroy();
        });
    }
}
