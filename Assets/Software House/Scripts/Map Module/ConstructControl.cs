using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UtilitySlot
{
    public bool uesd;
    public Transform transform;
}
public class ConstructControl : MonoBehaviour
{
    public List<UtilitySlot> utilitySlots;
    public Transform lookAtThis;
    public List<ConstructBase> constructBase_list;
    public float height;

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<ConstructBase>() != null)
            {
                constructBase_list.Add(transform.GetChild(i).GetComponent<ConstructBase>());
            }
        }
    }

    public UtilitySlot GetUtilitySlot()
    {
        foreach (var item in utilitySlots)
        {
            if (!item.uesd)
            {
                return item;
                
            }
        }

        return null;
    }

    public Transform GetLookAt()
    {
        if (lookAtThis == null)
        {
            return transform;
        }
        else
        {
            return lookAtThis;
        }
    }

    public void FindGround()
    {
        foreach (ConstructBase ConstructBase in constructBase_list)
        {
            ConstructBase.FindGround();
        }
    }

    public void ResetUnderConstructSlot()
    {
        foreach (ConstructBase ConstructBase in constructBase_list)
        {
            if (ConstructBase.constructSlot != null)
            {
                ConstructBase.constructSlot.aboveConstruct = null;
            }
        }
    }

    public void Move(Vector2 position)
    {
        transform.position = new Vector3(position.x, height, position.y);
    }

    public void Rotate()
    {
        transform.Rotate(new Vector3(0, 45, 0));
    }

    public void ReverseRotate()
    {
        transform.Rotate(new Vector3(0, -45, 0));
    }

    public void Placement()
    {
        foreach (ConstructBase ConstructBase in constructBase_list)
        {
            ConstructBase.constructSlot.aboveConstruct = this;
            ConstructBase.ResetHighlight();
        }
    }

    public void ResetHighlight()
    {
        foreach (ConstructBase ConstructBase in constructBase_list)
        {
            ConstructBase.ResetHighlight();
        }
    }

    public bool canPlacement
    {
        get { return CanPlacement(); }
    }


    private bool CanPlacement()
    {
        int counter = 0;

        foreach (ConstructBase ConstructBase in constructBase_list)
        {
            if (ConstructBase.canPlacement)
            {
                counter++;
            }
        }

        if (counter == constructBase_list.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}