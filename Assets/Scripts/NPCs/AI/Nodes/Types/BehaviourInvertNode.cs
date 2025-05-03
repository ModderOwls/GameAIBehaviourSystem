using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourInvertNode : BehaviourDecoratorNode
{
    public override string name { get; set; } = "Invert";


    public BehaviourInvertNode(BehaviourNode child)
    {
        this.child = child;
    }

    protected override void OnStart() { }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
        name = "!" + child.name;

        NodeState result = child.Update();

        switch (result)
        {
            case NodeState.Success:
                return NodeState.Failure;
            case NodeState.Failure:
                return NodeState.Success;
            case NodeState.Running:
                return NodeState.Running;
            default:
                return NodeState.Failure;
        }
    }
}
