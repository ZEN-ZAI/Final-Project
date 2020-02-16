using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EmployeeData
{
    public string employeeID;
    public int characterID;
    public string employeeName;

    public int salary;

    public int mood_max;
    public int mood_current;

    public int stamina_max;
    public int stamina_current;

    public EmployeeStatus employeeStatus;

    public int indexJob;
    public List<JobData> jobs = new List<JobData>();

    //public List<NatureData> naturesDatas;
    //public List<Certificate> certificates;
    //public List<Item> equipments;

    public string construct = "";

    public string stateAI;
    public string actionAI;
    public float x;
    public float y;
    public float z;
    public float rotation;

    public string workID = "";

    public void AddWork(string workID)
    {
        this.workID = workID;
    }

    public void RemoveWork()
    {
        this.workID = "";
    }

    public bool haveWork
    {
        get
        {
            if (this.workID == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public void MyEmployee()
    {
        employeeStatus = EmployeeStatus.MyEmployee;
    }

    public JobData GetPrimaryJob()
    {
        return jobs[indexJob];
    }

    public int GetAllJobLevel()
    {
        int temp = 0;

        foreach (var item in jobs)
        {
            temp+= item.level;
        }

        return temp;
    }

    public JobData Getjob(JobData jobData)
    {
        JobData jobData_temp = jobs.Find(e => e.jobType == jobData.jobType);

        if (jobData_temp != null)
        {
            return jobData_temp;
        }
        else
        {
            return null;
        }
    }

    public void AddJob(JobData jobData)
    {
        JobData jobData_temp = jobs.Find(e => e.jobType == jobData.jobType);

        if (jobData_temp != null && jobData_temp.jobType == jobData.jobType)
        {
            jobData_temp.level += jobData.level;
        }
        else
        {
            jobs.Add(jobData);
        }

    }

    public void SetJob(int index)
    {
        this.indexJob = index;
    }

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();

        result["employeeID"] = employeeID;
        result["characterID"] = characterID;
        result["employeeName"] = employeeName;

        result["salary"] = salary;

        result["mood_max"] = mood_max;
        result["mood_current"] = mood_current;

        result["stamina_max"] = stamina_max;
        result["stamina_current"] = stamina_current;

        result["employeeStatus"] = employeeStatus.GetHashCode();

        Dictionary<string, object> result_jobs = new Dictionary<string, object>();

        for (int i = 0; i < jobs.Count; i++)
        {
            result_jobs.Add(i + "", jobs[i].ToDictionary());
        }

        result["indexJob"] = indexJob;
        result["jobs"] = result_jobs;


        result["employeeID"] = employeeID;

        result["stateAI"] = stateAI;
        result["actionAI"] = actionAI;
        result["x"] = x;
        result["y"] = y;
        result["z"] = z;
        result["z"] = z;

        result["rotation"] = rotation;
        result["workID"] = workID;

        result["construct"] = construct;

        return result;
    }
}

[Serializable]
public class NatureData
{
    public NatureType natureType;
    public int level;

    public NatureData RandomNature(int maxLevel)
    {
        NatureData natureData = new NatureData();
        NatureType natureType = (NatureType)UnityEngine.Random.Range(1, Enum.GetValues(typeof(NatureType)).Length);
        natureData.natureType = natureType;
        natureData.level = UnityEngine.Random.Range(1, maxLevel);

        return natureData;
    }
}
