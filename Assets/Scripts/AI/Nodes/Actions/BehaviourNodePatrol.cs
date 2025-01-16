using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourNodePatrol : BehaviourActionNode
{
    BehaviourNodeGoTo nodeGoTo;

    Transform[] destinations;
    int current;

    public override string name { get; set; } = "Patrol";

    public BehaviourNodePatrol(NavMeshAgent agent, Transform[] destinations)
    {
        if (destinations.Length == 0)
        {
            Debug.LogError("Empty destination array was given, add atleast one.");

            agent.gameObject.SetActive(false);

            return;
        }

        this.destinations = destinations;
        nodeGoTo = new BehaviourNodeGoTo(agent, destinations[0].position);
    }

    protected override void OnStart() { }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
        if (nodeGoTo.Update() == NodeState.Success)
        {
            current++;
            if (current > destinations.Length - 1) current = 0;

            nodeGoTo.UpdateDestinationPosition(destinations[current].position);
        }

        return NodeState.Running;
    }
}
