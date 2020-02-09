using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Working", menuName = "Software House AI/Action/Working")]
public class Working : ActionAI
{
    public override void Act(StateController controller)
    {
        if (controller.employeeData.haveWork)
        {
            bool workIsDone = WorkManager.instance.WorkIsDone(controller.employeeData.workID);

            if (!workIsDone)
            {
                controller.AnimationWork();
                WorkManager.instance.Processing(controller.employeeData.workID);
            }
            else if (workIsDone)
            {
                EmployeeStructure.instance.GetMyEmployeeData(controller.employeeData.employeeID).RemoveWork();
            }
        }
    }
}
