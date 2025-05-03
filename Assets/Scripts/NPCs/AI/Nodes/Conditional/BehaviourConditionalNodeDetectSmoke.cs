using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Detects for smoke and succeeds if found.
/// 
/// BlackBoard interactions -
/// SET: SmokeObj (Transform), Hide (bool)
/// </summary>
public class BehaviourConditionalNodeDetectSmoke : BehaviourDecoratorNode
{
    public override string name { get; set; } = "Detecting Smoke";

    NavMeshAgent agent;
    BlackBoard blackBoard;
    LayerMask layerSmoke;


    public BehaviourConditionalNodeDetectSmoke(NavMeshAgent agent, BlackBoard blackBoard, BehaviourNode child)
    {
        this.agent = agent;
        this.blackBoard = blackBoard;
        this.child = child;

        layerSmoke = blackBoard.GetValue<LayerMask>("LayerSmoke");
    }

    protected override void OnStart() { }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
        name = "Detecting Smoke<br>" + child.name;

        Collider[] hits = new Collider[3];
        int count = Physics.OverlapSphereNonAlloc(agent.transform.position, 1f, hits, layerSmoke);

        for (int i = 0; i < count; i++)
        {
            if (hits[i].CompareTag("Smoke"))
            {
                Debug.Log(hits[i].name);

                blackBoard.SetValue("SmokeObj", hits[i].transform);
                blackBoard.SetValue("Hide", true);

                return NodeState.Success;
            }
        }

        return child.Update();
    }
}
