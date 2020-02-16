using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GetIn", menuName = "Software House AI/Action/GetIn")]
public class GetIn : ActionAI
{
    public override void Act(StateController controller)
    {
        if (controller.targetObject.GetComponent<ConstructSlot>() != null)
        {
            if (controller.targetObject.GetComponent<ConstructControl>().GetUtilitySlot() != null) // have utility slot
            {
                controller.utilitySlot = controller.targetObject.GetComponent<ConstructControl>().GetUtilitySlot();

                if (!controller.utilitySlot.uesd)
                {
                    controller.transform.position = controller.utilitySlot.transform.position;
                    controller.agent.StopMove();

                    controller.agent.RotateTowards(controller.targetObject.GetComponent<ConstructControl>().GetLookAt());

                    controller.utilitySlot.uesd = true;
                    MessageSystem.instance.UpdateMessage(controller.employeeData.employeeName + " ใช้งาน " + controller.targetObject.GetComponent<ConstructSlot>().construct.name);
                }

            }
            else if(controller.targetObject.GetComponent<ConstructControl>().GetUtilitySlot() == null)// not have utility slot
            {
                controller.agent.StopMove();
                controller.agent.RotateTowards(controller.targetObject.GetComponent<ConstructControl>().transform);
                MessageSystem.instance.UpdateMessage(controller.employeeData.employeeName + " ใช้งาน " + controller.targetObject.GetComponent<ConstructSlot>().construct.name);
            }
        }
    }
}