using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourNodePatrol : BehaviourNodeGoTo
{
    public override string name { get; set; } = "Patrol";

    Transform[] destinations;
    int current;


    public BehaviourNodePatrol(NavMeshAgent agent, Transform[] destinations) : base(agent, destinations[0].position)
    {
        if (destinations.Length == 0)
        {
            Debug.LogError("Empty destination array was given, add atleast one.");

            agent.gameObject.SetActive(false);

            return;
        }

        this.destinations = destinations;
    }

    protected override void OnStart()
    {
        UpdateDestinationPosition(destinations[current].position);
        name = "Patrol<br>" + name;

        base.OnStart();
    }

    protected override NodeState OnUpdate()
    {
        if (base.OnUpdate() == NodeState.Success)
        {
            current++;
            if (current > destinations.Length - 1) current = 0;

            UpdateDestinationPosition(destinations[current].position);
            name = "Patrol<br>" + name;
        }

        return NodeState.Running;
    }
}
