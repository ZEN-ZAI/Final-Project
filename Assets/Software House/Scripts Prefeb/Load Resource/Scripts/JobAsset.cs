using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class JobAsset : MonoBehaviour
{
    #region Singleton
    public static JobAsset instance;
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

    public List<Job> jobAsset;

    // Start is called before the first frame update
    void Start()
    {
        jobAsset = Resources.LoadAll<Job>("Job").ToList();
    }

    public Sprite GetJobSprite(JobType jobType)
    {
        return jobAsset.Find(e => e.jobType == jobType).icon;
    }
}