using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaminaFull", menuName = "Software House AI/Decision/StaminaFull")]
public class StaminaFull : Decision
{
    public override bool Decide(StateController controller)
    {
        if (controller.employeeData.stamina_current >= controller.employeeData.stamina_max)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
