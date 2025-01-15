using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree
{
    public BehaviourNode startNode;
    public BehaviourNode.NodeState state;

    bool startNodeExists;

    public BehaviourNode.NodeState Update()
    {
        if (!startNodeExists)
        {
            startNodeExists = startNode != null;

            if (!startNodeExists)
            {
                Debug.LogError("There is no start node added to this behaviour tree. Add one.");
            }
        }

        if (startNodeExists)
        {
            if (state == BehaviourNode.NodeState.Running)
                state = startNode.Update();
        }
        else
        {
            state = BehaviourNode.NodeState.Failure;
        }

        return state;
    }
}
