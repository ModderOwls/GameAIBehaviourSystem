using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourNodeFollow : BehaviourActionNode
{
    BehaviourNodeGoTo nodeGoTo;
    Transform target;

    float offset = 1;

    public override string name { get; set; } = "Follow";

    public BehaviourNodeFollow(NavMeshAgent agent, Transform target)
    {
        this.target = target;
        nodeGoTo = new BehaviourNodeGoTo(agent, target.position);
    }

    protected override void OnStart() { }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
        if (target == null) return NodeState.Failure;



        float distance = Vector3.Distance(nodeGoTo.agent.transform.position, target.position);

        if (distance > 4)
        {
            nodeGoTo.UpdateDestinationPosition(target.position + (nodeGoTo.agent.transform.position - target.position).normalized * offset);
        }

        nodeGoTo.Update();

        return NodeState.Running;
    }
}
