using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourNodeGoTo : BehaviourActionNode
{
    public override string name { get; set; } = "GoTo";

    public NavMeshAgent agent;


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

        //Rounded to make text shorter.
        name = "GoTo<br>" + new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
    }
}
