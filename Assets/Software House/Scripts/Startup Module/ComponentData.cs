using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ComponentData
{
    public string componentID;
    public int researchLevel;
    public int researchExp;

    public int developLevel;
    public int productLevel;

    public List<string> employee_Worker = new List<string>();

    public ComponentData()
    {
    }

    public ComponentData(Component component, int researchLevel, int productLevel)
    {
        this.componentID = component.componentID;
        this.researchLevel = researchLevel;
        this.productLevel = productLevel;
    }

    public void UpProductLevel()
    {
        if (productLevel < researchLevel)
        {
            productLevel++;
        }
    }

    public void DownProductLevel()
    {
        if (productLevel > 1)
        {
            productLevel--;
        }
    }

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["componentID"] = componentID;

        result["researchLevel"] = researchLevel;
        result["researchExp"] = researchExp;

        result["productLevel"] = productLevel;
        result["developLevel"] = developLevel;

        Dictionary<string, object> result_employee_Worker = new Dictionary<string, object>();
        for (int i = 0; i < employee_Worker.Count; i++)
        {
            result_employee_Worker.Add(i + "", employee_Worker[i]);
        }

        result["employee_Worker"] = result_employee_Worker;

        return result;
    }

    public void ProcessResearch()
    {
        foreach (string employeeID in employee_Worker)
        {
            UpdateResearchExp(1);
        }
    }

    public int researchMaxExp
    {
        get
        {
            if (researchLevel == 0)
            {
                return 10000;
            }
            else
            {
                return researchLevel * 10000;
            }
        }
    }

    public void AddMember(string employeeID)
    {
        employee_Worker.Add(employeeID);
    }

    public void RemoveMember(string employeeID)
    {
        employee_Worker.Remove(employeeID);
    }

    public void UpdateResearchExp(int exp)
    {
        researchExp += exp;

        if (researchExp >= researchMaxExp)
        {
            researchExp = 0;
            researchLevel++;
        }
    }

    public void UpdateDevelopLevel(int exp)
    {
        developLevel += exp;

        if (developLevel >= productLevel)
        {
            developLevel = productLevel;
        }
    }

    public ComponentData Clone()
    {
        return new ComponentData
        {
            componentID = this.componentID,
            developLevel = this.researchLevel,
            productLevel = 0
        };
    }

}