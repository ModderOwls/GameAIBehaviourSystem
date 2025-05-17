using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// BlackBoard interactions - 
/// SET: Weapon (Weapon)
/// </summary>
public class BehaviourNodeSearchWeapon : BehaviourNodeGoTo
{
    public override string name { get; set; } = "Searching weapon";

    BlackBoard blackBoard;
    Weapon weaponFound;


    public BehaviourNodeSearchWeapon(NavMeshAgent agent, BlackBoard blackBoard) : base(agent, agent.transform.position)
    {
        this.blackBoard = blackBoard;
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
        float nearestDistance = Vector3.Distance(agent.transform.position, nearestWeapon.position);
        for (int i = 1; i < weapons.Length; i++)
        {
            float distance = Vector3.Distance(agent.transform.position, weapons[i].transform.position);
            if (nearestDistance > distance)
            {
                nearestDistance = distance;

                nearestWeapon = weapons[i].transform;
            }
        }

        weaponFound = nearestWeapon.GetComponent<Weapon>();

        blackBoard.SetValue("Weapon", weaponFound);

        UpdateDestinationPosition(nearestWeapon.position);
        name = "Searching weapon<br>" + name;
    }

    protected override NodeState OnUpdate()
    {
        if (base.OnUpdate() == NodeState.Success)
        {
            GrabWeapon();

            return NodeState.Success;
        }

        return NodeState.Running;
    }

    private void GrabWeapon()
    {
        weaponFound.transform.parent = agent.transform;
        weaponFound.transform.localPosition = Vector2.zero;
        weaponFound.transform.localRotation = Quaternion.identity;
    }
}
