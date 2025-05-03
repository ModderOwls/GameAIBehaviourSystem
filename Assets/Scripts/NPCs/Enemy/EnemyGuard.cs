using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuard : NpcAI
{
    public Transform[] destinations;

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

        blackBoard.SetValue("LayerEnemy", (LayerMask)LayerMask.GetMask("Player"));
        blackBoard.SetValue("LayerSmoke", (LayerMask)LayerMask.GetMask("Projectile"));

        blackBoard.SetValue("Hide", false);

        //If 'Hide' is true in the blackboard, go hide.
        selector.children.Add(new BehaviourConditionalNodeWhile(blackBoard, "Hide", new BehaviourNodeHide(navAgent, "SmokeObj", blackBoard)));

        //Patrolling behaviour. Checks for smokes and enemies.
        BehaviourNodePatrol patrol = new BehaviourNodePatrol(navAgent, destinations);
        BehaviourInvertNode detectSmoke = new BehaviourInvertNode(new BehaviourConditionalNodeDetectSmoke(navAgent, blackBoard, patrol)); //Invert so it returns as a fail if it succeeds.
        BehaviourConditionalNodeDetectEnemy detectEnemy = new BehaviourConditionalNodeDetectEnemy(navAgent, "Player", blackBoard, detectSmoke);

        selector.children.Add(detectEnemy);

        //Search for a weapon and add to blackBoard.
        selector.children.Add(new BehaviourNodeSearchWeapon(navAgent, blackBoard));

        //Chase the target (player), throw projectile (60% chance) and attack using found weapon until losing the player.
        BehaviourNodeSequence attackSequence = new BehaviourNodeSequence();
        attackSequence.children.Add(new BehaviourConditionalNodeRandom(60, new BehaviourNodeThrowProjectile(navAgent.transform, blackBoard)));
        attackSequence.children.Add(new BehaviourNodeChase(navAgent, blackBoard));
        attackSequence.children.Add(new BehaviourNodeAttack(blackBoard));

        BehaviourNodeRepeat repeatAttackSequence = new BehaviourNodeRepeat();
        repeatAttackSequence.child = attackSequence;

        //Detect smoke again, now during the attack sequence.
        BehaviourInvertNode detectSmokeChase = new BehaviourInvertNode(new BehaviourConditionalNodeDetectSmoke(navAgent, blackBoard, repeatAttackSequence));

        //Upon losing the player, fail and restart the tree.
        //selector.children.Add(new BehaviourInvertNode(new BehaviourConditionalNodeLostSight(navAgent, blackBoard, attackSequence)));
        selector.children.Add(detectSmokeChase);

        behaviour.startNode = selector;
    }
}
