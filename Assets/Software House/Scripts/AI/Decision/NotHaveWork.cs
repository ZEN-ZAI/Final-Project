using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotHaveWork", menuName = "Software House AI/Decision/NotHaveWork")]
public class NotHaveWork : Decision
{
	public override bool Decide(StateController controller)
	{
		if (!controller.employeeData.haveWork)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}