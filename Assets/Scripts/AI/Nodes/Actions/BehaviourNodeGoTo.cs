using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourNodeGoTo : BehaviourActionNode
{
    public NavMeshAgent agent;

    public override string name { get; set; } = "GoTo";

    public BehaviourNodeGoTo(NavMeshAgent agent, Vector3 destination)
    {
        this.agent = agent;

        UpdateDestinationPosition(destination);
    }

    protected override void OnStart() { }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
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

    public void UpdateDestinationPosition(Vector3 position)
    {
        agent.SetDestination(position);
    }
}
