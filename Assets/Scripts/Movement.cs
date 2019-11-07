using System;
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
    private GameObject visionSphere;
    private Vector3 safePath;

    void Start()
    {
        // Create a vision sphere around each boid.
        visionSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        visionSphere.transform.position = transform.position;
        // Sets the vision spheres parent to the boid so that it moves with the boid
        visionSphere.transform.parent = transform;
        // Stop sphere being rendered and adds a trigger
        visionSphere.GetComponent<MeshRenderer>().enabled = false;
        visionSphere.GetComponent<SphereCollider>().isTrigger = true;

        // Scales vision sphere to be the same as the vision range
        visionSphere.transform.localScale = new Vector3(2 * visionRange, 2 * visionRange, 2 * visionRange);

        //sets default value of safePath
        safePath = transform.forward;
    }

    private void OnTriggerStay(Collider other)
    {
        other = visionSphere.GetComponent<Collider>();
        safePath = GetPathAwayFromObjects(fov, angle, visionRange);

        Debug.DrawRay(transform.position, safePath * visionRange, Color.yellow);
    }

    private void OnTriggerEnter(Collider other)
    {
        other = visionSphere.GetComponent<Collider>();
        safePath = GetPathAwayFromObjects(fov, angle, visionRange);

        Debug.DrawRay(transform.position, safePath * visionRange, Color.yellow);
    }

    void Update()
    {       
        transform.forward = Vector3.RotateTowards(transform.forward, safePath, turnSpeed, 0.0f);

        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }
}