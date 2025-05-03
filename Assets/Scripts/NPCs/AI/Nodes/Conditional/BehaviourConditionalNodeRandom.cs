using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BehaviourNode;

/// <summary>
/// Chance is in %. So passing 60 for the chance float will have a 60% chance of calling the child.
/// Chance is only done once, so if it passes it'll keep running until the child stops.
/// </summary>
public class BehaviourConditionalNodeRandom : BehaviourDecoratorNode
{
    public override string name { get; set; } = "Invert";

    float chance;
    float random;


    public BehaviourConditionalNodeRandom(float chance, BehaviourNode child)
    {
        this.chance = chance / 100;
        this.child = child;
    }

    protected override void OnStart()
    {
        random = Random.value;
    }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
        if (random >= chance)
        {
            return child.Update();
        }

        return NodeState.Success;
    }
}
