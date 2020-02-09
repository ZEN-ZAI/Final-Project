using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomToWorkObject", menuName = "Software House AI/Action/RandomToWorkObject")]
public class RandomToWorkObject : ActionAI
{
    public override void Act(StateController controller)
    {
        Transform targetObject = RandomWorkObject(controller.transform);

        controller.targetObject = targetObject.gameObject;
    }

    private Transform RandomWorkObject(Transform transform)
    {
        return MapManager.instance.RandomWorkObject(transform);
    }
}