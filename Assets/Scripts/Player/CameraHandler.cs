using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [Header("Settings")]

    public float smoothing;


    [Header("References")]

    public Transform player;


    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + new Vector3(0, 6, -4.75f), Time.deltaTime * smoothing);
    }
}
