using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourSelectorNode : BehaviourCompositeNode
{
    public override string name { get; set; } = "Selector";

    int current = 0;


    protected override void OnStart() { }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
        switch (children[current].Update())
        {
            case NodeState.Success:
                current++;

                if (current >= children.Count)
                {
                    current = children.Count - 1;
                }
                break;
            case NodeState.Failure:
                current = 0;
                break;
        }

        name = "Selector<br>" + children[current].name;
        return NodeState.Running;
    }
}
