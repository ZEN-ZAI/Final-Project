using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalkingToNearRelaxObject",menuName = "Software House AI/Action/WalkingToNearRelaxObject")]
public class WalkingToNearRelaxObject : ActionAI
{
    public override void Act(StateController controller)
    {
        Transform targetObject = FindNearRelaxObject(controller.transform);

        controller.AnimationWalk();
        controller.agent.MoveTo(targetObject.position);
        controller.targetObject = targetObject.gameObject;
    }

    private Transform FindNearRelaxObject(Transform transform)
    {
        return MapManager.instance.FindNearRelaxObject(transform);
    }
}