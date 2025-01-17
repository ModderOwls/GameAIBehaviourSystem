using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourNodeSearchWeapon : BehaviourNode
{
    public override string name { get; set; } = "Searching weapon";

    BehaviourNodeGoTo nodeGoTo;

    Weapon weaponFound;


    public BehaviourNodeSearchWeapon(NavMeshAgent agent)
    {
        nodeGoTo = new BehaviourNodeGoTo(agent, agent.transform.position);
        name += "<br>" + nodeGoTo.name;
    }

    protected override void OnStart()
    {
        GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");

        if (weapons.Length == 0)
        {
            Debug.LogError("No weapon found, please add one.");

            return;
        }

        Transform nearestWeapon = weapons[0].transform;
        float nearestDistance = Vector3.Distance(nodeGoTo.agent.transform.position, nearestWeapon.position);
        for (int i = 1; i < weapons.Length; i++)
        {
            float distance = Vector3.Distance(nodeGoTo.agent.transform.position, weapons[i].transform.position);
            if (nearestDistance > distance)
            {
                nearestDistance = distance;

                nearestWeapon = weapons[i].transform;
            }
        }

        weaponFound = nearestWeapon.GetComponent<Weapon>();

        nodeGoTo.UpdateDestinationPosition(nearestWeapon.position);
        name = "Searching weapon<br>" + nodeGoTo.name;
    }

    protected override void OnStop() { }

    protected override NodeState OnUpdate()
    {
        if (nodeGoTo == null) return NodeState.Failure;

        if (nodeGoTo.Update() == NodeState.Success)
        {
            GrabWeapon();

            return NodeState.Success;
        }

        return NodeState.Running;
    }

    private void GrabWeapon()
    {
        weaponFound.transform.parent = nodeGoTo.agent.transform;
        weaponFound.transform.localPosition = Vector2.zero;
        weaponFound.transform.localRotation = Quaternion.identity;
    }
}
