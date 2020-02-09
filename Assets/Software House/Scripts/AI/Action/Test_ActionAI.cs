using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Test_ActionAI", menuName = "Software House AI/Action/Test_ActionAI")]
public class Test_ActionAI : ActionAI
{
    public override void Act(StateController controller)
    {
        MessageSystem.instance.UpdateMessage("Test_action");
    }
}