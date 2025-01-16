using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourNodeAttack : BehaviourActionNode
{
    Weapon weapon;

    Transform owner;

    public override string name { get; set; } = "Attacking";

    public BehaviourNodeAttack(Transform owner)
    {
        this.owner = owner;
    }

    protected override void OnStart()
    {
        //Not a great solution, but works without ruining modularity.
        for (int i = 0; i < owner.childCount; ++i)
        {
            Transform child = owner.GetChild(i);
            if (child.CompareTag("Weapon"))
            {
                weapon = child.GetComponent<Weapon>();
            }
        }

        weapon.Attack();
    }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
        if (weapon == null)
        {
            Debug.LogError("Weapon not found.");

            return NodeState.Failure;
        }

        if (weapon.timer < 0)
        {
            return NodeState.Success;
        }

        return NodeState.Running;
    }
}
