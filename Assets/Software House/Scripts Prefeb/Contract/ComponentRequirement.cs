using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComponentRequirement : MonoBehaviour
{
    public SlotComponent slotComponent;
    public TMP_Text point;

    public void Set(ComponentData componentData)
    {
        slotComponent.DisplayOnlyMode(componentData);
        point.text = componentData.productLevel.ToString()+" Point";
    }
}