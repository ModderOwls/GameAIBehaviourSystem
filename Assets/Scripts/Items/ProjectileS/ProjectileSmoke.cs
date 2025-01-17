using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSmoke : Projectile
{
    public GameObject smokeParticles;

    void OnEnable()
    {
        stopped = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Transform particles = Instantiate(smokeParticles).transform;

        particles.position = transform.position;

        stopped = true;
        SetActive(false);
    }
}
