using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "New Component", menuName = "Software House/Component")]
public class Component : ScriptableObject
{
    public string componentID;
    public string componentName;
    public CategorieType categorieType;
    public ComponentType componentType;
    public Sprite icon;

    public string description;

    public List<JobType> job = new List<JobType>();

}