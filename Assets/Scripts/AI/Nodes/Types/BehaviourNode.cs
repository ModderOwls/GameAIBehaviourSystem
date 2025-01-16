using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourNode
{
    public bool started;
    [SerializeField] NodeState state;

    public virtual string name { get; set; }

    public enum NodeState
    {
        Running,
        Success,
        Failure
    }

    protected abstract void OnStart();

    protected abstract NodeState OnUpdate();

    protected abstract void OnStop();

    public NodeState Update()
    {
        if (!started)
        {
            OnStart();
            started = true;
        }

        state = OnUpdate();

        if (state == NodeState.Failure || state == NodeState.Success)
        {
            OnStop();
            started = false;
        }

        return state;
    }
}
