using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GetIn", menuName = "Software House AI/Action/GetIn")]
public class GetIn : ActionAI
{
    public override void Act(StateController controller)
    {
        controller.agent.StopMove();
        MessageSystem.instance.UpdateMessage("ActionAI -> Get in "+controller.targetObject.name);
    }
}