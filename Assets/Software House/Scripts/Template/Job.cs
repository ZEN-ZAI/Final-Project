using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "New Job", menuName = "Software House/Job")]
public class Job : ScriptableObject
{
    public JobType jobType;
    public Sprite icon;
}
