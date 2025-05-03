using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourParallelNode : BehaviourCompositeNode
{
    public override string name { get; set; } = "Parallel (0)";


    protected override void OnStart() { }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
        name = "Parallel (" + children.Count + "):<br>";

        for (int i = 0; i < children.Count; ++i)
        {
            NodeState childState = children[i].Update();

            name += children[i].name;

            if (childState != NodeState.Running)
            {
                return childState;
            }
        }

        return NodeState.Running;
    }
}
