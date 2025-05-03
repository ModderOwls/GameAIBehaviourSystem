using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Returns the child node's state if given key starts true.
/// It detects it each Update, unlike Random, so if you're running another node parallel,
/// you can change the blackboard's value from the outside to affect this node.
/// 
/// BlackBoard interactions -
/// GET: (Given 'key)
/// </summary>
public class BehaviourConditionalNodeWhile : BehaviourDecoratorNode
{
    public override string name { get; set; } = "While";

    BlackBoard blackBoard;
    string key;

    bool value;


    public BehaviourConditionalNodeWhile(BlackBoard blackBoard, string key, BehaviourNode child)
    {
        this.blackBoard = blackBoard;
        this.key = key;
        this.child = child;
    }

    protected override void OnStart() { }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
        if (blackBoard.GetValue<bool>(key))
        {
            name = "While (" + key + ")<br>" + child.name;
            return child.Update();
        }
        else
        {
            return NodeState.Success;
        }
    }
}
