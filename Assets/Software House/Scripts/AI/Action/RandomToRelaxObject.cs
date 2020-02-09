using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomToRelaxObject", menuName = "Software House AI/Action/RandomToRelaxObject")]
public class RandomToRelaxObject : ActionAI
{
    public override void Act(StateController controller)
    {
        Transform targetObject = RandomRelaxObject(controller.transform);

        controller.targetObject = targetObject.gameObject;
    }

    private Transform RandomRelaxObject(Transform transform)
    {
        return MapManager.instance.RandomRelaxObject(transform);
    }
}