using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behaviour node that ALWAYS returns as a failure.
/// 
/// Can be useful for selector nodes.
/// </summary>
public class BehaviourFailNode : BehaviourNode
{
    public override string name { get; set; } = "Fail";

    public int debugID;

    protected override void OnStart() { }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
        return NodeState.Failure;
    }
}
