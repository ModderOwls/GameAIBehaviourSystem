using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourSelectorNodeEnemyGuard : BehaviourCompositeNode
{
    public int current;

    NavMeshAgent agent;

    PlayerController player;
    Transform goToObject;

    LayerMask layerPlayer;

    public override string name { get; set; } = "Selector Guard";

    public BehaviourSelectorNodeEnemyGuard(NavMeshAgent agent, Transform[] destinations)
    {
        this.agent = agent;

        goToObject = new GameObject().transform;

        children.Add(new BehaviourNodePatrol(agent, destinations));
        children.Add(new BehaviourNodeSearchWeapon(agent));
        children.Add(new BehaviourNodeChase(agent, goToObject));
        children.Add(new BehaviourNodeAttack(agent.transform));

        layerPlayer = LayerMask.GetMask("Player");
    }

    protected override void OnStart() { }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
        switch (children[current].Update())
        {
            case NodeState.Success:
                current++;

                if (current > children.Count - 1)
                {
                    current = children.Count - 2;
                }
                break;
            case NodeState.Failure:
                current = 0;

                goToObject.parent = null;
                player = null;
                break;
        }

        name = "Selector Guard<br>" + children[current].name;

        if (player == null)
        {
            //Searches for player(s if i reuse this) using a sphere.
            Collider[] hitColliders = new Collider[3];
            int numColliders = Physics.OverlapSphereNonAlloc(agent.transform.position, 3, hitColliders, layerPlayer);
            if (numColliders > 0)
            {
                player = hitColliders[0].GetComponent<PlayerController>();
                goToObject.parent = player.transform;
                goToObject.localPosition = Vector3.zero;

                current = 1;
            }
        }

        return NodeState.Running;
    }
}
