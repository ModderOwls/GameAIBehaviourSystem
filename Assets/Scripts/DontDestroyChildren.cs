using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyChildren : MonoBehaviour
{
    public void OnParticleSystemStopped()
    {
        transform.DetachChildren();

        Destroy(gameObject);
    }
}
