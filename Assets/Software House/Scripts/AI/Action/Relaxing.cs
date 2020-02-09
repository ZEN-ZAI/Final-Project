using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relexing", menuName = "Software House AI/Action/Relaxing")]
public class Relaxing : ActionAI
{
    public override void Act(StateController controller)
    {
        controller.AnimationIdle();

        if (controller.employeeData.stamina_current < controller.employeeData.stamina_max)
        {
            controller.employeeData.stamina_current += 1;

            if (controller.employeeData.stamina_current > controller.employeeData.stamina_max)
            {
                controller.employeeData.stamina_current = controller.employeeData.stamina_max;
            }
        }
        
    }
}
