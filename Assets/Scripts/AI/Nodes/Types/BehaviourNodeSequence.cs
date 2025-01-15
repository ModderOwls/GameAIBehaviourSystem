using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourNodeSequence : BehaviourCompositeNode
{
    private int current;

    protected override void OnStart()
    {
        current = 0;
    }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
        if (children.Count >= 1)
        {
            switch (children[current].Update())
            {
                case NodeState.Running:
                    return NodeState.Running;
                case NodeState.Success:
                    ++current;
                    break;
                case NodeState.Failure:
                    return NodeState.Failure;
            }
        }
        else
        {
            Debug.LogError("Behaviour node sequence has no children.");

            return NodeState.Failure;
        }

        return current == children.Count ? NodeState.Success : NodeState.Running;
    }
}
