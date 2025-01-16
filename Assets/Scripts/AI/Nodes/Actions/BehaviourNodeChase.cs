using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourNodeChase : BehaviourActionNode
{
    BehaviourNodeGoTo nodeGoTo;
    Transform target;

    float offset = 0.9f;

    Weapon weapon;

    LayerMask layerGround;

    public override string name { get; set; } = "Chase";

    public BehaviourNodeChase(NavMeshAgent agent, Transform target)
    {
        this.target = target;
        nodeGoTo = new BehaviourNodeGoTo(agent, target.position);

        layerGround = LayerMask.GetMask("Ground");
    }

    protected override void OnStart()
    {
        weapon = nodeGoTo.agent.transform.GetChild(0).GetComponent<Weapon>();
    }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
        if (target == null) return NodeState.Failure;

        nodeGoTo.UpdateDestinationPosition(target.position + (nodeGoTo.agent.transform.position - target.position).normalized * offset);

        nodeGoTo.Update();

        float distance = Vector3.Distance(nodeGoTo.agent.transform.position, target.position);

        if (distance > 5)
        {
            //Simple sight line that only checks for colliders with the Ground layer.
            //Added this as it was too easy to lose the npc that it made debugging the smoke hard.
            if (Physics.Raycast(nodeGoTo.agent.transform.position, target.position, layerGround))
            {
                return NodeState.Failure;
            }
        }

        if (distance < 2f)
        {
            return NodeState.Success;
        }

        return NodeState.Running;
    }
}
