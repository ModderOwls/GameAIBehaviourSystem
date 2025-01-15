using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class EnemyGuard : EnemyAI
{
    public Transform destination;

    EnemyWeapon weapon;

    new void Start()
    {
        base.Start();

        behaviour = new BehaviourTree();

        BehaviourNodeSequence sequence = new BehaviourNodeSequence();
        sequence.children.Add(new BehaviourDebugNode(1));
        sequence.children.Add(new BehaviourNodeGoTo(navAgent, destination));
        sequence.children.Add(new BehaviourDebugNode(3));

        BehaviourNodeRepeat repeater = new BehaviourNodeRepeat();
        repeater.child = sequence;

        behaviour.startNode = repeater;
    }
}
