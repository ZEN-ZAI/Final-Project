using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResetUtility", menuName = "Software House AI/Action/ResetUtility")]
public class ResetUtility : ActionAI
{
    public override void Act(StateController controller)
    {
        if (controller.utilitySlot != null)
        {
            controller.utilitySlot.uesd = false;
        }
    }
}