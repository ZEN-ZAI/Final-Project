using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DestinationComplete", menuName = "Software House AI/Decision/DestinationComplete")]
public class DestinationComplete : Decision
{
	public float distance = 10f;

	public override bool Decide(StateController controller)
	{
		if (Vector3.Distance(controller.targetObject.transform.position, controller.transform.position) <= distance)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}