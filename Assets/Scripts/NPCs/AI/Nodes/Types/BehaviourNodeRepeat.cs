using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourNodeRepeat : BehaviourDecoratorNode
{
    protected override void OnStart() { }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
        name = "Repeat:<br>" + child.name;

        child.Update();
        return NodeState.Running;
    }
}
