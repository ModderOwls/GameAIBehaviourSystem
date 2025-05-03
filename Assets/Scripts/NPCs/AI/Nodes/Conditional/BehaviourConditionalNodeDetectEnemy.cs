using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Detects for enemies using an object tag and succeeds if found.
/// 
/// BlackBoard interactions -
/// SET: Target (Transform)
/// </summary>
public class BehaviourConditionalNodeDetectEnemy : BehaviourDecoratorNode
{
    public override string name { get; set; } = "Detecting Enemy";

    NavMeshAgent agent;
    string enemyTag;
    BlackBoard blackBoard;
    LayerMask layerEnemy;


    public BehaviourConditionalNodeDetectEnemy(NavMeshAgent agent, string enemyTag, BlackBoard blackBoard, BehaviourNode child)
    {
        this.agent = agent;
        this.enemyTag = enemyTag;
        this.blackBoard = blackBoard;
        this.child = child;

        layerEnemy = blackBoard.GetValue<LayerMask>("LayerEnemy");
    }

    protected override void OnStart() { }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
        name = "Detecting Enemy<br>" + child.name;

        Collider[] hits = new Collider[4];
        int count = Physics.OverlapSphereNonAlloc(agent.transform.position, 3f, hits, layerEnemy);

        for (int i = 0; i < count; i++)
        {
            if (hits[i].CompareTag(enemyTag))
            {
                blackBoard.SetValue("Target", hits[i].transform);

                return NodeState.Success;
            }
        }

        return child.Update();
    }
}
