using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class EnemyGuard : NpcAI
{
    public Transform[] destinations;

    EnemyWeapon weapon;

    new void Start()
    {
        base.Start();

        behaviour = new BehaviourTree();

        behaviour.startNode = new BehaviourSelectorNodeEnemyGuard(navAgent, destinations);
    }
}
