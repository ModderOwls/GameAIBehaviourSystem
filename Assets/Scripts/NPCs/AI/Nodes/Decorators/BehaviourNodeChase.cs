using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourNodeChase : BehaviourNode
{
    public override string name { get; set; } = "Chase";

    BehaviourNodeGoTo nodeGoTo;
    Transform target;

    float offset = 0.9f;

    Weapon weapon;

    LayerMask layerObstacles;


    public BehaviourNodeChase(NavMeshAgent agent, Transform target)
    {
        this.target = target;

        nodeGoTo = new BehaviourNodeGoTo(agent, target.position);
        name += "<br>" + nodeGoTo.name;

        layerObstacles = LayerMask.GetMask("Obstacles");
    }

    protected override void OnStart()
    {
        weapon = nodeGoTo.agent.transform.GetChild(0).GetComponent<Weapon>();
    }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
        if (target == null) return NodeState.Failure;

        Vector3 position = nodeGoTo.agent.transform.position;

        nodeGoTo.UpdateDestinationPosition(target.position + (position - target.position).normalized * offset);
        name = "Chase<br>" + nodeGoTo.name;

        nodeGoTo.Update();

        float distance = Vector3.Distance(position, target.position);

        if (distance > 5)
        {
            RaycastHit hit;

            //Simple sight line that only checks for colliders with the Ground layer.
            //Added this as it was too easy to lose the npc that it made debugging the smoke hard.
            Debug.DrawRay(position, target.position - position, Color.yellow);
            if (Physics.Raycast(position, target.position - position, out hit, Vector3.Distance(position, target.position), layerObstacles))
            {
                Debug.Log(hit.collider.name);

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
