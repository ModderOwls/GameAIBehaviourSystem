using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// BlackBoard interactions -
/// GET: HideObj (Transform)
/// SET: Hide (bool)
/// </summary>
public class BehaviourNodeHide : BehaviourNodeGoTo
{
    public override string name { get; set; } = "Hiding";

    string hideObj;
    BlackBoard blackBoard;
    Obstacle obstacleFound;
    Transform hidingFrom;

    public BehaviourNodeHide(NavMeshAgent agent, string hideObj, BlackBoard blackBoard) : base(agent, agent.transform.position)
    {
        this.hideObj = hideObj;
        this.blackBoard = blackBoard;
    }

    protected override void OnStart()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacles");

        if (obstacles.Length == 0)
        {
            Debug.LogError("No obstacles found, please add one.");

            return;
        }

        Vector3 position = agent.transform.position;

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

        hidingFrom = blackBoard.GetValue<Transform>(hideObj);

        UpdateDestinationPosition(nearestObstacle.position - (hidingFrom.position - nearestObstacle.position).normalized * (obstacleFound.radius + 0.5f));
        name = "Hiding<br>" + name;
    }

    protected override void OnStop()
    {
        blackBoard.SetValue("Hide", false);
    }
}
