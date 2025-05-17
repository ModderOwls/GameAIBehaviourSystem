using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// BlackBoard interactions -
/// GET: Weapon (Weapon), Target (Transform)
/// </summary>
public class BehaviourNodeChase : BehaviourNodeGoTo
{
    public override string name { get; set; } = "Chase";

    Transform target;

    float offset = 0.9f;

    BlackBoard blackBoard;
    Weapon weapon;

    LayerMask layerObstacles;


    public BehaviourNodeChase(NavMeshAgent agent, BlackBoard blackBoard) : base(agent, agent.nextPosition)
    {
        this.blackBoard = blackBoard;

        layerObstacles = LayerMask.GetMask("Obstacles");
    }

    protected override void OnStart()
    {
        weapon = blackBoard.GetValue<Weapon>("Weapon");

        target = blackBoard.GetValue<Transform>("Target");

        base.OnStart();
    }

    protected override NodeState OnUpdate()
    {
        if (target == null) return NodeState.Failure;

        Vector3 position = agent.transform.position;

        UpdateDestinationPosition(target.position + (position - target.position).normalized * offset);
        name = "Chase<br>" + name;

        base.OnUpdate();

        float distance = Vector3.Distance(position, target.position);

        if (distance > 5)
        {
            RaycastHit hit;

            //Simple sight line that only checks for colliders with the Ground layer.
            //Added this as it was too easy to lose the npc that it made debugging the smoke hard.
            Debug.DrawRay(position, target.position - position, Color.yellow);
            if (Physics.Raycast(position, target.position - position, out hit, Vector3.Distance(position, target.position), layerObstacles))
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
