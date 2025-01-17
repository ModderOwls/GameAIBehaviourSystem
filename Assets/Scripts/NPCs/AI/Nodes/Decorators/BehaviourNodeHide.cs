using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourNodeHide : BehaviourNode
{
    public override string name { get; set; } = "Hiding";

    BehaviourNodeGoTo nodeGoTo;

    Obstacle obstacleFound;
    Transform hidingFrom;

    public BehaviourNodeHide(NavMeshAgent agent, Transform hidingFrom)
    {
        this.hidingFrom = hidingFrom;

        nodeGoTo = new BehaviourNodeGoTo(agent, agent.transform.position);
        name += "<br>" + nodeGoTo.name;
    }

    protected override void OnStart()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacles");

        if (obstacles.Length == 0)
        {
            Debug.LogError("No obstacles found, please add one.");

            return;
        }

        Vector3 position = nodeGoTo.agent.transform.position;

        Transform nearestObstacle = obstacles[0].transform;
        float nearestDistance = Vector3.Distance(position, nearestObstacle.position);
        for (int i = 1; i < obstacles.Length; i++)
        {
            float distance = Vector3.Distance(position, obstacles[i].transform.position);
            if (nearestDistance > distance)
            {
                nearestDistance = distance;

                nearestObstacle = obstacles[i].transform;
            }
        }

        obstacleFound = nearestObstacle.GetComponent<Obstacle>();

        nodeGoTo.UpdateDestinationPosition(nearestObstacle.position - (hidingFrom.position - nearestObstacle.position).normalized * (obstacleFound.radius + 0.5f));
        name = "Hiding<br>" + nodeGoTo.name;
    }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
        if (nodeGoTo == null) return NodeState.Failure;

        if (nodeGoTo.Update() == NodeState.Success)
        {
            return NodeState.Success;
        }

        return NodeState.Running;
    }
}
