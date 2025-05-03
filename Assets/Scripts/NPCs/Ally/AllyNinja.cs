using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyNinja : NpcAI
{
    public PlayerController player;

    public Projectile prefabProjectile;

    new void Start()
    {
        base.Start();

        Projectile projectile = Instantiate(prefabProjectile);
        //For some reason Unity is not calling this by itself?
        projectile.Awake();

        behaviour = new BehaviourTree();

        BlackBoard blackBoard = new BlackBoard();
        BehaviourSelectorNode selector = new BehaviourSelectorNode();

        blackBoard.SetValue("Projectile", projectile);
        blackBoard.SetValue("Target", player.transform);

        blackBoard.SetValue("LayerEnemy", (LayerMask)LayerMask.GetMask("NPCs"));

        //Detects for the enemy Guard, if it finds it continues, otherwise follows infinitely.
        selector.children.Add(new BehaviourConditionalNodeDetectEnemy(navAgent, "Enemy", blackBoard, new BehaviourNodeFollow(navAgent, player.transform)));
        
        //Hide and throw a smoke bomb.
        selector.children.Add(new BehaviourNodeHide(navAgent, "Target", blackBoard));
        selector.children.Add(new BehaviourNodeThrowProjectile(navAgent.transform, blackBoard));
        selector.children.Add(new BehaviourFailNode()); //Reset the behaviour.

        behaviour.startNode = selector;
    }
}
