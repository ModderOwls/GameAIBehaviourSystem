using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// BlackBoard interactions -
/// GET: Weapon (Weapon)
/// </summary>
public class BehaviourNodeAttack : BehaviourActionNode
{
    public override string name { get; set; } = "Attacking";

    BlackBoard blackBoard;

    Weapon weapon;


    public BehaviourNodeAttack(BlackBoard blackBoard)
    {
        this.blackBoard = blackBoard;
    }

    protected override void OnStart()
    {
        weapon = blackBoard.GetValue<Weapon>("Weapon");

        weapon?.Attack();
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
