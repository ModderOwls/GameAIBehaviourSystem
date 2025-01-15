using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourNodeGoTo : BehaviourActionNode
{
    NavMeshAgent agent;
    Transform destination;

    public BehaviourNodeGoTo(NavMeshAgent agent, Transform destination)
    {
        this.agent = agent;
        this.destination = destination;
    }

    protected override void OnStart() { }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
        agent.SetDestination(destination.position);

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return NodeState.Success;
                }
            }
        }

        return NodeState.Running;
    }
}
