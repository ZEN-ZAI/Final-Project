using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalkingToNearWorkObject", menuName = "Software House AI/Action/WalkingToNearWorkObject")]
public class WalkingToNearWorkObject : ActionAI
{
    public override void Act(StateController controller)
    {
        Transform targetObject = FindNearWorkObject(controller.transform);

        controller.AnimationWalk();
        controller.agent.MoveTo(targetObject.position);
        controller.targetObject = targetObject.gameObject;
    }

    private Transform FindNearWorkObject(Transform transform)
    {
        return MapManager.instance.FindNearWorkObject(transform);
    }
}