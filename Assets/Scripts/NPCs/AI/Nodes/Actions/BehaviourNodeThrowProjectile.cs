using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// Behaviour node that throws a projectile at an area.
/// 
/// BlackBoard interactions - 
/// GET: Projectile (Projectile), Target (Transform)
/// </summary>
public class BehaviourNodeThrowProjectile : BehaviourActionNode
{
    public override string name { get; set; } = "Throwing projectile";

    Transform owner;
    Transform target;

    Projectile projectile;

    BlackBoard blackBoard;

    float timer;

    public BehaviourNodeThrowProjectile(Transform owner, BlackBoard blackBoard)
    {
        this.owner = owner;
        this.blackBoard = blackBoard;
    }

    protected override void OnStart()
    {
        timer = 0;

        projectile = blackBoard.GetValue<Projectile>("Projectile");
        target = blackBoard.GetValue<Transform>("Target");

        if (projectile == null) return;

        projectile.transform.position = owner.position;

        projectile.SetActive(true);
        projectile.LandAt(target.position, 45);
    }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
        if (target == null || projectile == null)
        {
            Debug.LogError("Throwing target or Projectile not found.");

            return NodeState.Failure;
        }

        timer += Time.fixedDeltaTime;

        if (projectile.stopped || timer > 5)
        {
            projectile.SetActive(false);
            return NodeState.Success;
        }

        return NodeState.Running;
    }
}
