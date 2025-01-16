using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourSelectorNodeAllyNinja : BehaviourCompositeNode
{
    public int current;

    NavMeshAgent agent;

    PlayerController player;
    Transform goToObject;

    LayerMask layerPlayer;

    public override string name { get; set; } = "Selector Ninja";

    public BehaviourSelectorNodeAllyNinja(NavMeshAgent agent, PlayerController player)
    {
        this.agent = agent;

        goToObject = new GameObject().transform;

        children.Add(new BehaviourNodeFollow(agent, player.transform));

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

        name = "Selector Ninja<br>" + children[current].name;

        return NodeState.Running;
    }
}
