using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractListPanel : MonoBehaviour
{
    #region Singleton
    public static ContractListPanel instance;

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
    public GameObject slotContract;

    // Start is called before the first frame update
    void Start()
    {
        RefreshSlotContract();
    }

    public void RefreshSlotContract()
    {
        RemoveAllContract();
        AddSlotContract();
    }

    private void AddSlotContract()
    {
        List<ContractData> contractDatas = WorkStructure.instance.GetOutSourceWorkList();

        for (int i = 0; i < CompanyStructure.instance.GetMaxWork().maxContract; i++)
        {
            if (i < contractDatas.Count)
            {
                ContractData contractData_temp = contractDatas[i];
                SlotContract SlotContract_temp = Instantiate(slotContract, content).GetComponent<SlotContract>();

                SlotContract_temp.Set(contractData_temp);
                SlotContract_temp.ShowContractMode();
            }
            else
            {
                Instantiate(slotContract, content).GetComponent<SlotContract>().AddContractMode();
            }
        }
    }

    private void RemoveAllContract()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
