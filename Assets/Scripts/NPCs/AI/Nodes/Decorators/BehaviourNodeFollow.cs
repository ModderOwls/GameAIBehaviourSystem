using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourNodeFollow : BehaviourNodeGoTo
{
    public override string name { get; set; } = "Follow";

    Transform target;

    readonly static float offset = 2;


    public BehaviourNodeFollow(NavMeshAgent agent, Transform target) : base(agent, target.position + (agent.transform.position - target.position).normalized * offset)
    {
        this.target = target;
    }

    protected override NodeState OnUpdate()
    {
        if (target == null) return NodeState.Failure;

        float distance = Vector3.Distance(agent.transform.position, target.position);

        if (distance > 4)
        {
            UpdateDestinationPosition(target.position + (agent.transform.position - target.position).normalized * offset);
            name = "Follow<br>" + name;
        }

        base.OnUpdate();

        return NodeState.Running;
    }
}
