using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaminaFullAndHaveWork", menuName = "Software House AI/Decision/StaminaFullAndHaveWork")]
public class StaminaFullAndHaveWork : Decision
{
    public override bool Decide(StateController controller)
    {
        if (controller.employeeData.haveWork && controller.employeeData.stamina_current >= controller.employeeData.stamina_max)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
