using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float zOffset;
    public float yOffset;
    public float followDelay;
    private Vector3 velocity;

    void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(target.position.x, target.position.y + yOffset, zOffset);

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, followDelay);
    }
}
