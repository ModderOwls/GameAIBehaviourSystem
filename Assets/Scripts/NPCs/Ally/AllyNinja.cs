using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

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

        behaviour.startNode = new BehaviourSelectorNodeAllyNinja(navAgent, player, projectile);
    }
}
