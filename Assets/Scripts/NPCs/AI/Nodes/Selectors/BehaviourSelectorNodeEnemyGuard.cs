using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Behaviour node that acts as an enemy guard.
/// 
/// Requires a NavMeshAgent and an array of patrolling destinations.
/// </summary>
public class BehaviourSelectorNodeEnemyGuard : BehaviourCompositeNode
{
    public override string name { get; set; } = "Selector Guard";

    public int current;

    NavMeshAgent agent;

    PlayerController player;

    Transform goToObject;
    Transform hideObject;

    LayerMask layerPlayer;
    LayerMask layerSmoke;

    float sightTimer;


    public BehaviourSelectorNodeEnemyGuard(NavMeshAgent agent, Transform[] destinations, Projectile projectile)
    {
        this.agent = agent;

        goToObject = new GameObject().transform;
        hideObject = new GameObject().transform;

        children.Add(new BehaviourNodeHide(agent, hideObject));
        children.Add(new BehaviourNodePatrol(agent, destinations));
        children.Add(new BehaviourNodeSearchWeapon(agent));
        children.Add(new BehaviourNodeThrowProjectile(agent.transform, goToObject, projectile));
        children.Add(new BehaviourNodeChase(agent, goToObject));
        children.Add(new BehaviourNodeAttack(agent.transform));

        current = 1;

        layerPlayer = LayerMask.GetMask("Player");
        layerSmoke = LayerMask.GetMask("Projectile");
    }

    protected override void OnStart() { }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
        switch (children[current].Update())
        {
            case NodeState.Success:
                current++;

                if (current > children.Count - 1)
                {
                    int attackLoopLength = 2;
                    //Gives a chance to throw glue.
                    attackLoopLength += Mathf.RoundToInt(Random.value * .6f);
                    current = children.Count - attackLoopLength;
                }
                break;
            case NodeState.Failure:
                current = 1;

                goToObject.parent = null;
                player = null;

                sightTimer = 1;
                break;
        }

        name = "Selector Guard<br>" + children[current].name;

        //Ensure player doesn't immediately get found again after losing sight.
        if (sightTimer > 0) sightTimer -= Time.fixedDeltaTime;

        DetectPlayer();
        DetectSmoke();

        return NodeState.Running;
    }

    void DetectPlayer()
    {
        if (sightTimer > 0) return;

        if (player == null)
        {
            //Searches for player(s if i reuse this) using a sphere.
            Collider[] hitColliders = new Collider[3];
            int numColliders = Physics.OverlapSphereNonAlloc(agent.transform.position, 3, hitColliders, layerPlayer);
            
            if (numColliders == 0) return;

            player = hitColliders[0].GetComponent<PlayerController>();

            goToObject.parent = player.transform;
            goToObject.localPosition = Vector3.zero;

            current = 2; //Search for weapon.
        }
        else
        {
            if (!player.enabled)
            {
                current = 1;
            }
        }
    }

    void DetectSmoke()
    {
        if (hideObject.parent == null)
        {
            //Searches for smokes using a sphere. Only needs one.
            Collider[] hitColliders = new Collider[1];
            int numColliders = Physics.OverlapSphereNonAlloc(agent.transform.position, 1, hitColliders, layerSmoke);
            
            if (numColliders == 0) return;

            if (!hitColliders[0].CompareTag("Smoke")) return;

            Transform smoke = hitColliders[0].transform;

            hideObject.parent = smoke.transform;
            hideObject.localPosition = Vector3.zero;

            player = null;
            sightTimer = 3;

            current = 0; //Get scared and go into hiding.
        }
    }
}
