using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    protected BehaviourTree behaviour = new BehaviourTree();
    protected NavMeshAgent navAgent;

    protected void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    protected void FixedUpdate()
    {
        behaviour.Update();
    }
}
