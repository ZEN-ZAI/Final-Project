using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public abstract void Execute(Agent Agent, bool forward);
}

public class PerformMoveWithKey : Command
{
    public override void Execute(Agent Agent, bool forward)
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));
        Agent.Move(direction);
        //Agent.AnimationRun();
    }
}

public class PerformMoveWithMouse : Command
{
    public override void Execute(Agent Agent, bool forward)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Agent.MoveTo(hit.point);
            //Agent.AnimationRun();
        }
    }
}

public class PerformIdle : Command
{
    public override void Execute(Agent Agent, bool forward)
    {
       // Agent.AnimationIdle();
    }
}

public class PerformDash : Command
{
    public override void Execute(Agent Agent, bool forward)
    {

    }
}

public class DoNothing : Command
{
    public override void Execute(Agent Agent, bool forward)
    {

    }
}


