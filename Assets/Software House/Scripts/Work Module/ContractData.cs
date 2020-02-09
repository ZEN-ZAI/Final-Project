using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// small 1-3 วัน ประมาณ 3-9 นาที
// medium 4-10 วัน ประมาณ 12-30 นาที
// Large 11-20 วัน ประมาณ 33-60 นาที

[Serializable]
public class ContractData
{
    public string workID;

    public ContractType contractType;
    public ScaleType scaleType;

    public List<ComponentData> genreDatas = new List<ComponentData>();
    public List<ComponentData> themeDatas = new List<ComponentData>();
    public List<ComponentData> cameraDatas = new List<ComponentData>();
    public List<ComponentData> graphicDatas = new List<ComponentData>();
    public List<ComponentData> platformDatas = new List<ComponentData>();
    public List<ComponentData> featureDatas = new List<ComponentData>();

    public Data.GameTimeStructure dueDate;
    public Reward reward = new Reward();
    public Fine fine = new Fine();

    public int processCurrent;
    public int processMax;

    public List<string> employee_Worker = new List<string>();

    public ContractStatus contractStatus = ContractStatus.None;

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["workID"] = workID;
        result["contractType"] = contractType.GetHashCode();
        result["scaleType"] = scaleType.GetHashCode();

        Dictionary<string, object> result_genreDatas = new Dictionary<string, object>();
        for (int i = 0; i < genreDatas.Count; i++)
        {
            result_genreDatas.Add(i + "", genreDatas[i].ToDictionary());
        }
        result["genreDatas"] = result_genreDatas;

        Dictionary<string, object> result_themeDatas = new Dictionary<string, object>();
        for (int i = 0; i < themeDatas.Count; i++)
        {
            result_themeDatas.Add(i + "", themeDatas[i].ToDictionary());
        }
        result["themeDatas"] = result_themeDatas;

        Dictionary<string, object> result_cameraDatas = new Dictionary<string, object>();
        for (int i = 0; i < cameraDatas.Count; i++)
        {
            result_cameraDatas.Add(i + "", cameraDatas[i].ToDictionary());
        }
        result["cameraDatas"] = result_cameraDatas;

        Dictionary<string, object> result_graphicDatas = new Dictionary<string, object>();
        for (int i = 0; i < graphicDatas.Count; i++)
        {
            result_graphicDatas.Add(i + "", graphicDatas[i].ToDictionary());
        }
        result["graphicDatas"] = result_graphicDatas;

        Dictionary<string, object> result_platformDatas = new Dictionary<string, object>();
        for (int i = 0; i < platformDatas.Count; i++)
        {
            result_platformDatas.Add(i + "", platformDatas[i].ToDictionary());
        }
        result["platformDatas"] = result_platformDatas;

        Dictionary<string, object> result_featureDatas = new Dictionary<string, object>();
        for (int i = 0; i < featureDatas.Count; i++)
        {
            result_featureDatas.Add(i + "", featureDatas[i].ToDictionary());
        }
        result["featureDatas"] = result_featureDatas;

        result["dueDate"] = dueDate.ToDictionary();
        result["reward"] = reward.ToDictionary();
        result["fine"] = fine.ToDictionary();

        Dictionary<string, object> result_employee_Worker = new Dictionary<string, object>();

        for (int i = 0; i < employee_Worker.Count; i++)
        {
            result_employee_Worker.Add(i + "", employee_Worker[i]);
        }

        result["employee_Worker"] = result_employee_Worker;

        result["processCurrent"] = processCurrent;
        result["processMax"] = processMax;

        result["contractStatus"] = contractStatus.GetHashCode();

        return result;
    }

    public void AddMember(string employeeID)
    {
        employee_Worker.Add(employeeID);
    }

    public void RemoveMember(string employeeID)
    {
        employee_Worker.Remove(employeeID);
    }

    public void RemoveAllMember()
    {
        employee_Worker.Clear();
    }

    public bool isDone
    {
        get
        {
            if (processCurrent == processMax)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void Process()
    {
        foreach (string employeeID in employee_Worker)
        {
            if (ProcessComponent(employeeID, genreDatas))
            {
                continue;
            }
            else if (ProcessComponent(employeeID, themeDatas))
            {
                continue;
            }
            else if (ProcessComponent(employeeID, cameraDatas))
            {
                continue;
            }
            else if (ProcessComponent(employeeID, graphicDatas))
            {
                continue;
            }
            else if (ProcessComponent(employeeID, platformDatas))
            {
                continue;
            }
            else if (ProcessComponent(employeeID, featureDatas))
            {
                continue;
            }
        }
    }

    private bool ProcessComponent(string employeeID, List<ComponentData> componentDatas)
    {
        bool update = false; ;
        foreach (ComponentData componentData in componentDatas)
        {
            int power = EmployeeManager.instance.GetPowerByEmployeeID(employeeID, componentData);

            if (power != 0 && componentData.developLevel < componentData.productLevel)
            {
                componentData.UpdateDevelopLevel(power);
                update = true;
                break;
            }
        }

        return update;
    }

    public void UpdateProcessCurrent()
    {
        int temp_processCurrent = 0;

        foreach (ComponentData componentData in genreDatas)
        {
            temp_processCurrent += componentData.developLevel;
        }

        foreach (ComponentData componentData in themeDatas)
        {
            temp_processCurrent += componentData.developLevel;
        }

        foreach (ComponentData componentData in cameraDatas)
        {
            temp_processCurrent += componentData.developLevel;
        }

        foreach (ComponentData componentData in graphicDatas)
        {
            temp_processCurrent += componentData.developLevel;
        }

        foreach (ComponentData componentData in platformDatas)
        {
            temp_processCurrent += componentData.developLevel;
        }

        foreach (ComponentData componentData in featureDatas)
        {
            temp_processCurrent += componentData.developLevel;
        }

        processCurrent = temp_processCurrent;
    }

    public void Obtain()
    {
        contractStatus = ContractStatus.Obtain;
    }

    public void ClaimReward()
    {
        CompanyStructure.instance.AddCompanyMoney(reward.money);
        CompanyStructure.instance.AddCompanyExp(reward.exp);

        foreach (string employeeID in employee_Worker)
        {
            EmployeeStructure.instance.GetMyEmployeeData(employeeID).RemoveWork();
        }

        RemoveAllMember();
        contractStatus = ContractStatus.Claim;
        WorkStructure.instance.RemoveOutSourceWork(this);
    }

    public void Fine()
    {
        CompanyStructure.instance.RemoveCompanyMoney(fine.money);

        foreach (string employeeID in employee_Worker)
        {
            EmployeeStructure.instance.GetMyEmployeeData(employeeID).RemoveWork();
        }

        RemoveAllMember();
        contractStatus = ContractStatus.Fine;
        WorkStructure.instance.RemoveOutSourceWork(this);
    }

    public ContractData Clone()
    {
        return new ContractData
        {
            workID = this.workID,

            contractType = this.contractType,
            scaleType = this.scaleType,

            genreDatas = this.genreDatas,
            themeDatas = this.themeDatas,
            cameraDatas = this.cameraDatas,
            graphicDatas = this.graphicDatas,
            platformDatas = this.platformDatas,
            featureDatas = this.featureDatas,

            dueDate = this.dueDate,
            reward = this.reward,
            fine = this.fine,

            processCurrent = this.processCurrent,
            processMax = this.processMax
        };
    }
}

[Serializable]
public class Reward
{
    public int money;
    public int exp;

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["money"] = money;
        result["exp"] = exp;

        return result;
    }
}

[Serializable]
public class Fine
{
    public int money;

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["money"] = money;

        return result;
    }
}
