using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourNodeWait : BehaviourActionNode
{
    private float duration = 1f;

    private float timer;

    public override string name { get; set; }

    public BehaviourNodeWait(float duration)
    {
        this.duration = duration;

        name = "Wait seconds: " + duration;
    }

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
