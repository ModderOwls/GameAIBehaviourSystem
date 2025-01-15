using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourNodeWait : BehaviourActionNode
{
    public float duration = 1f;

    private float timer;

    protected override void OnStart()
    {
        timer = 0;
    }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (timer > duration) return NodeState.Success;

        return NodeState.Running;
    }
}
