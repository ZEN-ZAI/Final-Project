using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HaveWork", menuName = "Software House AI/Decision/HaveWork")]
public class HaveWork : Decision
{
	public override bool Decide(StateController controller)
	{
		if (controller.employeeData.haveWork)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}