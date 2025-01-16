using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class AllyNinja : NpcAI
{
    public PlayerController player;

    EnemyWeapon weapon;

    new void Start()
    {
        base.Start();

        behaviour = new BehaviourTree();

        behaviour.startNode = new BehaviourSelectorNodeAllyNinja(navAgent, player);
    }
}
