using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 1;
    public float cooldown;

    [HideInInspector] public float timer;

    public WeaponCollider weaponCollider;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (timer < 0)
        {
            Stop();

            enabled = false;

            return;
        }

        timer -= Time.fixedDeltaTime;
    }

    public virtual void Attack()
    {
        enabled = true;
        weaponCollider.coll.enabled = true;

        timer = cooldown;

        animator.Play("WeaponAttack", 0);
    }

    public virtual void Stop()
    {
        weaponCollider.coll.enabled = false;
    }
}
