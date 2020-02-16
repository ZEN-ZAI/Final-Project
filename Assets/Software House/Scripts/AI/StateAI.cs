using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Software House AI/State")]
public class StateAI : ScriptableObject
{
    public ActionAI[] actions;
    public ActionAI[] exitActions;

    public TransitionAI[] transitions;
    //public Color sceneGizmoColor = Color.gray;

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransition(controller);
    }

    private void DoActions(StateController controller)
    {
        foreach (ActionAI action in actions)
        {
            action.Act(controller);
            controller.current_ActionAI = action;
        }
    }

    public void ExitActions(StateController controller)
    {
        foreach (ActionAI action in exitActions)
        {
            action.Act(controller);
            controller.current_ActionAI = action;
        }
    }

    private void CheckTransition(StateController controller)
    {
        foreach (TransitionAI transition in transitions)
        {
            bool decisionSucceeded = transition.decision.Decide(controller);
            //controller.TransitionToState(decisionSucceeded ? transition.trueState : transition.falseState);

            if (decisionSucceeded)
            {
                ExitActions(controller);
                controller.TransitionToState(transition.trueState);
            }
            else
            {
                controller.TransitionToState(transition.falseState);
            }
        }
    }
}