using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaminaZero", menuName = "Software House AI/Decision/StaminaZero")]
public class StaminaZero : Decision
{
	public override bool Decide(StateController controller)
	{
		if (controller.employeeData.stamina_current == 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}