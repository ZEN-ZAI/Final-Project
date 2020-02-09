using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JobData
{
    public JobType jobType;
    public int exp;
    public int level;

    public int GetFullExp()
    {
        return level * 100;
    }

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();

        result["jobType"] = jobType.GetHashCode();
        result["exp"] = exp;
        result["level"] = level;

        return result;
    }

    public JobData RandomJobData(int maxLevel)
    {
        JobData jobData = new JobData();
        jobData.jobType = (JobType)UnityEngine.Random.Range(1, Enum.GetValues(typeof(JobType)).Length-2);
        jobData.level = UnityEngine.Random.Range(1, maxLevel+1);

        return jobData;
    }
}
