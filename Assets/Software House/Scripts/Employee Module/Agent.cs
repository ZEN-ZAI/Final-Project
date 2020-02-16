using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Agent : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Vector3 moveDirection;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetSpeed(int speed)
    {
        agent.speed = speed;
    }

    public bool hasPath
    {
        get { return agent.hasPath; }
    }

    public void MoveTo(Vector3 position)
    {
        this.targetPosition = position;
        agent.SetDestination(targetPosition);
    }

    public void StopMove()
    {
        agent.SetDestination(this.transform.position);
    }

    public void Move(Vector3 direction)
    {
        this.moveDirection = direction;
        agent.Move(moveDirection);
    }

    public void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation,Time.deltaTime * 100f);
    }
}
