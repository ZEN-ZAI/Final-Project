using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

namespace Data
{
    [Serializable]
    public class EmployeeStructure
    {
        public Dictionary<string, EmployeeData> recruitmentEmployee = new Dictionary<string, EmployeeData>();
        public Dictionary<string, EmployeeData> myEmployee = new Dictionary<string, EmployeeData>();

        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            Dictionary<string, object> result_recruitmentEmployee = new Dictionary<string, object>();
            foreach (var item in recruitmentEmployee)
            {
                result_recruitmentEmployee.Add(item.Key, item.Value.ToDictionary());
            }

            result["recruitmentEmployee"] = result_recruitmentEmployee;


            Dictionary<string, object> result_myEmployee = new Dictionary<string, object>();
            foreach (var item in myEmployee)
            {
                result_myEmployee.Add(item.Key, item.Value.ToDictionary());
            }

            result["myEmployee"] = result_myEmployee;

            return result;
        }
    }
}

public class EmployeeStructure : MonoBehaviour
{
    #region Singleton
    public static EmployeeStructure instance;

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

    [SerializeField] private Data.EmployeeStructure employeeStructure;

    public bool set;

    public void Set(string json)
    {
        if (json == null)
        {
            employeeStructure = new Data.EmployeeStructure();
        }
        else
        {
            try
            {
                employeeStructure = JsonConvert.DeserializeObject<Data.EmployeeStructure>(json);
            }
            catch (Exception ex)
            {
                MessageSystem.instance.UpdateMessage(ex.ToString());
            }
            
        }

        set = true;
    }

    public string GetJson()
    {
        return JsonConvert.SerializeObject(employeeStructure);
    }


    public bool ContainMyEmployeeData(int employeeID)
    {
        return employeeStructure.myEmployee.ContainsKey(employeeID + "");
    }

    public EmployeeData GetMyEmployeeData(string employeeID)
    {
        return employeeStructure.myEmployee[employeeID];
    }

    public EmployeeData GetRecruitmentEmployeeData(int employeeID)
    {
        return employeeStructure.recruitmentEmployee[employeeID + ""];
    }

    public Data.EmployeeStructure GetEmployeeStructure()
    {
        return employeeStructure;
    }

    public int GetRecruitmentEmployeeCount()
    {
        return employeeStructure.recruitmentEmployee.Count;
    }

    public int GetMyEmployeeCount()
    {
        return employeeStructure.myEmployee.Count;
    }

    public List<EmployeeData> GetMyEmployeeDataList()
    {
        List<EmployeeData> temp = new List<EmployeeData>();
        foreach (var item in employeeStructure.myEmployee)
        {
            temp.Add(item.Value);
        }

        return temp;
    }

    public void AddEmployeeData(EmployeeData employeeData)
    {
        employeeStructure.myEmployee.Add(employeeData.employeeID + "", employeeData);
        //AddSkillDictionary(employeeData);
    }

    public void RemoveEmployeeData(EmployeeData employeeData)
    {
        employeeStructure.myEmployee.Add(employeeData.employeeID + "", employeeData);
        //RemoveSkillDictionary(employeeData);
    }

    public void ClearEmployeeData()
    {
        employeeStructure.myEmployee.Clear();
    }

    // Recruitment

    public List<EmployeeData> GetRecruitmentEmployeeDataList()
    {
        List<EmployeeData> temp = new List<EmployeeData>();
        foreach (var item in employeeStructure.recruitmentEmployee)
        {
            temp.Add(item.Value);
        }

        return temp;
    }

    public void AddRecruitmentEmployeeData(EmployeeData employeeData)
    {
        employeeStructure.recruitmentEmployee.Add(employeeData.employeeID+"", employeeData);
    }

    public void RemoveRecruitmentEmployeeData(EmployeeData employeeData)
    {
        employeeStructure.recruitmentEmployee.Add(employeeData.employeeID+"", employeeData);
    }

    public void ClearRecruitmentEmployeeData()
    {
        employeeStructure.recruitmentEmployee.Clear();
    }


}