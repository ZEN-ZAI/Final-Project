using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "New Furniture", menuName = "Software House/Construct")]
public class Construct : ScriptableObject
{
    public int constructID;
    public Sprite icon;
    public new string name = "New Construct";

    public ConstructType constructType;
    public string description;
    public GameObject constructPrefab;
}