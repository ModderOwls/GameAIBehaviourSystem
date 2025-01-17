using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

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

        behaviour.startNode = new BehaviourSelectorNodeEnemyGuard(navAgent, destinations, projectile);
    }
}
