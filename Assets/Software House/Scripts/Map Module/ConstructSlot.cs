using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructSlot : MonoBehaviour
{
    public ConstructData constructData;
    public Construct construct;

    public ConstructControl aboveConstruct;
    
    public void UpdateInfomation()
    {
        UpdatePosition();
        UpdateRotation();
        Updateheight();
        UpdateConstructID();
    }

    public void UpdateConstructID()
    {
        constructData.constructID = construct.constructID;
    }

    public void UpdatePosition()
    {
        constructData.x = (int)transform.position.x;
        constructData.z = (int)transform.position.z;
    }

    public void UpdateRotation()
    {
        constructData.rotation = (int)transform.rotation.eulerAngles.y;
    }

    public void Updateheight()
    {
        constructData.y = (int)transform.position.y;
    }

}