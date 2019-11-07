using System.Collections.Generic;
using UnityEngine;

using static AbstractAverageVectors;
using static AbstractMirrorVectorInPlane;

public class AbstractVisionCone : MonoBehaviour
{
    private Vector3[] CreatePoints(float fov, float angle)
    {
        /////////// If fov and angle wont change then this can be moved to Start() to improve performance
        int raysXPlane = (int)(fov / angle);
        int raysPerRotation = (int)(360 / angle);
        int rayDirectionSize;

        rayDirectionSize = (int)Mathf.Ceil(raysPerRotation * raysXPlane + 2);
        Vector3[] rayDirection = new Vector3[rayDirectionSize];
        //////////

        rayDirection[0] = transform.TransformDirection(Vector3.forward);

        for (int i = 0; i < raysXPlane; i++)
        {
            rayDirection[i * raysPerRotation + 1] = Quaternion.AngleAxis(angle * (i + 1), gameObject.transform.up) * rayDirection[0];

            for (int j = 0; j < raysPerRotation; j++)
            {
                rayDirection[i * raysPerRotation + j + 2] = Quaternion.AngleAxis(angle, gameObject.transform.forward) * rayDirection[i * raysPerRotation + j + 1];
            }

        }

        return rayDirection;
    }

    private List<Vector3> GetRayHits(Vector3[] rayDirections, float visionRange)
    {
        List<Vector3> hitDirections = new List<Vector3>();

        for (int i = 0; i < rayDirections.Length; i++)
        {
            if (Physics.Raycast(transform.position, rayDirections[i], visionRange))
            {
                hitDirections.Add(rayDirections[i]);
                Debug.DrawRay(transform.position, rayDirections[i] * visionRange, Color.red);
            }
            else
            {
                Debug.DrawRay(transform.position, rayDirections[i] * visionRange, Color.green);
            }
        }

        return hitDirections;
    }

    private List<Vector3> CheckVisionForObjects(float fov, float angle, float visionRange)
    {
        Vector3[] rayDirections = CreatePoints(fov, angle);
        List<Vector3> hitDirections = GetRayHits(rayDirections, visionRange);

        return hitDirections;
    }

    private Vector3 GetAverageCollisionUnitVector3(float fov, float angle, float visionRange, out bool objectsDetected)
    {
        List<Vector3> hitDirections = CheckVisionForObjects(fov, angle, visionRange);
        Vector3 newPath;

        if (hitDirections.Count == 0)
        {
            objectsDetected = false;
            newPath = transform.forward;
        }
        else
        {
            objectsDetected = true;
            newPath = AverageOfVector3List(hitDirections);
            newPath.Normalize();
        }

        return newPath;
    }

    public Vector3 GetPathAwayFromObjects(float fov, float angle, float visionRange)
    {
        Vector3 safePath = GetAverageCollisionUnitVector3(fov, angle, visionRange, out bool objectsDetected);

        if (objectsDetected)
        {
            safePath = Vector3.Reflect(-safePath, transform.forward);
        }

        return safePath;
    }
}
