using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Agent))]
[RequireComponent(typeof(AnimationController))]
public class StateController : MonoBehaviour
{
    public Agent agent;
    private AnimationController animationController;
    public Character character;
    public EmployeeData employeeData;

    public GameObject targetObject;
    public UtilitySlot utilitySlot;

    public StateAI current_StateAI;
    public ActionAI current_ActionAI;

    void Start()
    {
        agent = GetComponent<Agent>();
        animationController = GetComponent<AnimationController>();

        agent.SetSpeed(character.speed);

        print("employeeData.construct "+ employeeData.construct);
        if (employeeData.construct != "")
        {
            targetObject = GameObject.Find(employeeData.construct);
        }

        current_StateAI = AIAsset.instance.FindState(employeeData.stateAI);
        current_ActionAI = AIAsset.instance.FindAction(employeeData.actionAI);
    }

    void Update()
    {
        if (current_StateAI != null)
        {
            print(current_StateAI.ToString());
            current_StateAI.UpdateState(this);
            UpdateEmployeeData();
        }
    }

    private void UpdateEmployeeData()
    {
        employeeData.stateAI = current_StateAI.ToString();

        if (current_ActionAI != null)
        {
            employeeData.actionAI = current_ActionAI.ToString();
        }
        
        employeeData.rotation = transform.rotation.y;
        employeeData.x = transform.position.x;
        employeeData.y = transform.position.y;
        employeeData.z = transform.position.z;

        if (targetObject != null)
        {
            employeeData.construct = targetObject.name.ToString();
        }
    }

    public void TransitionToState(StateAI new_StateAI)
    {
        current_StateAI = new_StateAI;
    }

    public void AnimationWalk()
    {
        animationController.Walk();
    }

    public void AnimationIdle()
    {
        animationController.Idle();
    }

    public void AnimationWork()
    {
        animationController.Work();
    }

    public void LookAt(Transform transform)
    {
        transform.LookAt(transform);
    }
}
