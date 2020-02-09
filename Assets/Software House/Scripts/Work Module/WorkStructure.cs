using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Newtonsoft.Json;

namespace Data
{
    [Serializable]
    public class WorkStructure
    {
        public Dictionary<string, ContractData> contractScenarioType = new Dictionary<string, ContractData>();
        public Dictionary<string, ContractData> contractGraphicType = new Dictionary<string, ContractData>();
        public Dictionary<string, ContractData> contractModuleType = new Dictionary<string, ContractData>();
        public Dictionary<string, ContractData> contractSupportType = new Dictionary<string, ContractData>();
        public Dictionary<string, ContractData> contractFullGameProjectType = new Dictionary<string, ContractData>();

        public Dictionary<string, ContractData> outSourceWork = new Dictionary<string, ContractData>();

        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            Dictionary<string, object> result_contractScenarioType = new Dictionary<string, object>();
            foreach (var item in contractScenarioType)
            {
                result_contractScenarioType.Add(item.Key, item.Value.ToDictionary());
            }
            result["contractScenarioType"] = result_contractScenarioType;


            Dictionary<string, object> result_contractGraphicType = new Dictionary<string, object>();
            foreach (var item in contractGraphicType)
            {
                result_contractGraphicType.Add(item.Key, item.Value.ToDictionary());
            }
            result["contractGraphicType"] = result_contractGraphicType;


            Dictionary<string, object> result_contractModuleType = new Dictionary<string, object>();
            foreach (var item in contractModuleType)
            {
                result_contractModuleType.Add(item.Key, item.Value.ToDictionary());
            }
            result["contractModuleType"] = result_contractModuleType;


            Dictionary<string, object> result_contractSupportType = new Dictionary<string, object>();
            foreach (var item in contractSupportType)
            {
                result_contractSupportType.Add(item.Key, item.Value.ToDictionary());
            }
            result["contractSupportType"] = result_contractSupportType;

            Dictionary<string, object> result_contractFullGameProjectType = new Dictionary<string, object>();
            foreach (var item in contractFullGameProjectType)
            {
                result_contractFullGameProjectType.Add(item.Key, item.Value.ToDictionary());
            }
            result["contractFullGameProjectType"] = result_contractFullGameProjectType;


            Dictionary<string, object> result_outSourceWork = new Dictionary<string, object>();
            foreach (var item in outSourceWork)
            {
                result_outSourceWork.Add(item.Key, item.Value.ToDictionary());
            }
            result["outSourceWork"] = result_outSourceWork;

            return result;
        }
    }
}

public class WorkStructure : MonoBehaviour
{
    #region Singleton
    public static WorkStructure instance;

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

    [SerializeField] private Data.WorkStructure workStructure;

    public bool set;

    public void Set(string json)
    {
        if (json == null)
        {
            workStructure = new Data.WorkStructure();
        }
        else
        {
            workStructure = JsonConvert.DeserializeObject<Data.WorkStructure>(json);
        }
        set = true;
    }

    public string GetJson()
    {
        return JsonConvert.SerializeObject(workStructure);
    }

    public Data.WorkStructure GetWorkStructure()
    {
        return workStructure;
    }

    public int GetOutSourceWorkCount()
    {
        return workStructure.outSourceWork.Count;
    }

    public int GetContractScenarioTypeCount()
    {
        return workStructure.contractScenarioType.Count;
    }

    public int GetContractGraphicTypeCount()
    {
        return workStructure.contractGraphicType.Count;
    }

    public int GetContractModuleTypeCount()
    {
        return workStructure.contractModuleType.Count;
    }

    public int GetContractSupportTypeCount()
    {
        return workStructure.contractSupportType.Count;
    }

    public int GetContractFullGameProjectTypeCount()
    {
        return workStructure.contractFullGameProjectType.Count;
    }

    // My project
    public void AddOutSourceWork(ContractData contractData)
    {
        workStructure.outSourceWork.Add(contractData.workID, contractData);
    }

    public void RemoveOutSourceWork(ContractData contractData)
    {
        workStructure.outSourceWork.Remove(contractData.workID);
    }

    public bool ContainsOutsourceWork(string workID)
    {
        return workStructure.outSourceWork.ContainsKey(workID);
    }

    //Contract

    //ScenarioType
    public void AddContractScenarioType(ContractData contractData)
    {
        workStructure.contractScenarioType.Add(contractData.workID, contractData);
    }

    public void RemoveContractScenarioType(ContractData contractData)
    {
        workStructure.contractScenarioType.Remove(contractData.workID);
    }

    public void ClearContractScenarioType()
    {
        workStructure.contractScenarioType.Clear();
    }

    public List<ContractData> GetContractScenarioTypeList()
    {
        List<ContractData> temp = new List<ContractData>();
        foreach (var item in workStructure.contractScenarioType)
        {
            temp.Add(item.Value);
        }

        return temp;
    }

    //GraphicType
    public void AddContractGraphicType(ContractData contractData)
    {
        workStructure.contractGraphicType.Add(contractData.workID, contractData);
    }

    public void RemoveContractGraphicType(ContractData contractData)
    {
        workStructure.contractGraphicType.Remove(contractData.workID);
    }

    public void ClearContractGraphicType()
    {
        workStructure.contractGraphicType.Clear();
    }

    public List<ContractData> GetContractGraphicTypeList()
    {
        List<ContractData> temp = new List<ContractData>();
        foreach (var item in workStructure.contractGraphicType)
        {
            temp.Add(item.Value);
        }

        return temp;
    }

    //ModuleType
    public void AddContractModuleType(ContractData contractData)
    {
        workStructure.contractModuleType.Add(contractData.workID, contractData);
    }

    public void RemoveContractModuleType(ContractData contractData)
    {
        workStructure.contractModuleType.Remove(contractData.workID);
    }

    public void ClearContractModuleType()
    {
        workStructure.contractModuleType.Clear();
    }

    public List<ContractData> GetContractModuleTypeList()
    {
        List<ContractData> temp = new List<ContractData>();
        foreach (var item in workStructure.contractModuleType)
        {
            temp.Add(item.Value);
        }

        return temp;
    }

    //SupportType
    public void AddContractSupportType(ContractData contractData)
    {
        workStructure.contractSupportType.Add(contractData.workID, contractData);
    }

    public void RemoveContractSupportType(ContractData contractData)
    {
        workStructure.contractSupportType.Remove(contractData.workID);
    }

    public void ClearContractSupportType()
    {
        workStructure.contractSupportType.Clear();
    }

    public List<ContractData> GetContractSupportTypeList()
    {
        List<ContractData> temp = new List<ContractData>();
        foreach (var item in workStructure.contractSupportType)
        {
            temp.Add(item.Value);
        }

        return temp;
    }

    //FullGameProjectType
    public void AddContractFullGameProjectType(ContractData contractData)
    {
        workStructure.contractFullGameProjectType.Add(contractData.workID, contractData);
    }

    public void RemoveContractFullGameProjectType(ContractData contractData)
    {
        workStructure.contractFullGameProjectType.Remove(contractData.workID);
    }

    public void ClearContractFullGameProjectType()
    {
        workStructure.contractFullGameProjectType.Clear();
    }

    public List<ContractData> GetContractFullGameProjectTypeList()
    {
        List<ContractData> temp = new List<ContractData>();
        foreach (var item in workStructure.contractFullGameProjectType)
        {
            temp.Add(item.Value);
        }

        return temp;
    }

    //

    public List<ContractData> GetOutSourceWorkList()
    {
        List<ContractData> temp = new List<ContractData>();
        foreach (var item in workStructure.outSourceWork)
        {
            temp.Add(item.Value);
        }

        return temp;
    }

    public ContractData GetOutSourceWork(string workID)
    {
        if (workStructure.outSourceWork.ContainsKey(workID))
        {
            return (ContractData)workStructure.outSourceWork[workID];
        }
        else
        {
            return null;
        }
    }
}