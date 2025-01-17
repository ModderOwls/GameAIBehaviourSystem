using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Behaviour node that acts as an ally ninja.
/// 
/// Requires a NavMeshAgent, a player and a projectile prefab to throw.
/// </summary>
public class BehaviourSelectorNodeAllyNinja : BehaviourCompositeNode
{
    public override string name { get; set; } = "Selector Ninja";

    public int current;

    NavMeshAgent agent;

    PlayerController player;
    Projectile projectile;

    Transform hideObject;

    Transform enemyFound;

    LayerMask layerNPCs;
    
    //Enemy guard selector and ally ninja are very similar,
    //Kept separate because they're too specific to be used anywhere else.
    //Making them one would ironically be worse for modularity if i want to make changes to the npcs' selection behaviour.

    public BehaviourSelectorNodeAllyNinja(NavMeshAgent agent, PlayerController player, Projectile projectile)
    {
        this.agent = agent;
        this.player = player;
        this.projectile = projectile;

        hideObject = new GameObject().transform;

        children.Add(new BehaviourNodeFollow(agent, player.transform));
        children.Add(new BehaviourNodeHide(agent, hideObject));
        children.Add(new BehaviourNodeThrowProjectile(agent.transform, player.transform, projectile));
        children.Add(new BehaviourFailNode()); //Reset the behaviour.

        layerNPCs = LayerMask.GetMask("NPCs");
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
                    current = children.Count - 2;
                }
                break;
            case NodeState.Failure:
                current = 0;

                hideObject.parent = null;
                enemyFound = null;
                break;
        }

        name = "Selector Ninja<br>" + children[current].name;

        Detection();

        return NodeState.Running;
    }

    void Detection()
    {
        if (enemyFound == null)
        {
            //Searches for npcs using a sphere.
            Collider[] hitColliders = new Collider[4];
            int numColliders = Physics.OverlapSphereNonAlloc(agent.transform.position, 3, hitColliders, layerNPCs);
            for (int i = 1; i < numColliders; i++)
            {
                if (hitColliders[i].CompareTag("Enemy"))
                {
                    enemyFound = hitColliders[i].transform;
                    hideObject.parent = enemyFound.transform;
                    hideObject.localPosition = Vector3.zero;

                    current = 1;
                }
            }
        }
    }
}
