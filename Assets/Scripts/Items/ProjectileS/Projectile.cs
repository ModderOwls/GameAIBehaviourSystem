using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Projectile : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public SphereCollider coll;

    [HideInInspector] public bool stopped;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<SphereCollider>();
    }

    public void LandAt(Vector3 location, float angle)
    {
        Vector3 direction = location - transform.position;
        float height = direction.y;
        direction.y = 0;
        float distance = direction.magnitude;
        float angleRad = angle * Mathf.Deg2Rad;
        direction.y = distance * Mathf.Tan(angleRad);
        distance += height / Mathf.Tan(angleRad);

        float speed = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * angleRad));

        rb.velocity = speed * direction.normalized;
    }

    public virtual void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
