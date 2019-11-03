using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : AbstractVisionCone
{
    public int movementSpeed;
    public float turnSpeed;
    public float visionRange;
    public float angle;
    public float fov;

    void Start()
    {


    }

    void Update()
    {
        Vector3 safePath = GetPathAwayFromObjects(fov, angle, visionRange);

        Debug.DrawRay(transform.position, safePath * visionRange, Color.yellow);

        transform.forward = Vector3.RotateTowards(transform.forward, safePath, turnSpeed, 0.0f);

        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }
}
