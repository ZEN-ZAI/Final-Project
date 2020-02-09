using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalkTo", menuName = "Software House AI/Action/WalkTo")]
public class WalkTo : ActionAI
{
    public override void Act(StateController controller)
    {
        controller.AnimationWalk();
        controller.agent.MoveTo(controller.targetObject.transform.position);
    }
}
