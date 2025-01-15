using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourDebugNode : BehaviourNode
{
    public int debugID;

    public BehaviourDebugNode(int debugID)
    {
        this.debugID = debugID;
    }

    protected override void OnStart()
    {
        Debug.Log("OnStart called. ID: " + debugID);
    }

    protected override void OnStop()
    {
        Debug.Log("OnStop called. ID: " + debugID);
    }

    protected override NodeState OnUpdate()
    {
        Debug.Log("OnUpdate called. ID: " + debugID);
        return NodeState.Success;
    }
}
