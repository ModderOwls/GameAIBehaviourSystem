using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [Header("Settings")]

    public float smoothing;
    public Vector3 offset = new Vector3(0, 6, -6.5f);


    [Header("References")]

    public Transform player;


    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + offset, Time.deltaTime * smoothing);
    }
}
