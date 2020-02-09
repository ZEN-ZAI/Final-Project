using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConstructData
{
    public int constructID;
    public int x;
    public float y;
    public int z;
    public float rotation;

    public void Set(int constructID, int x, int z, int rotation, float y)
    {
        this.constructID = constructID;
        this.x = x;
        this.y = y;
        this.z = z;
        this.rotation = rotation;
    }

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["constructID"] = constructID;
        result["x"] = x;
        result["y"] = y;
        result["z"] = z;
        result["rotation"] = rotation;

        return result;
    }
}