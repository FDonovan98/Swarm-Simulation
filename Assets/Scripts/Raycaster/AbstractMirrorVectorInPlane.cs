using UnityEngine;

public class AbstractMirrorVectorInPlane : MonoBehaviour
{
    public static Vector3 MirrorVector(Vector3 vector, Vector3 plane)
    {
        Vector3 normal = vector - plane;

        Vector3 mirroredVector = vector - 2 * normal * Vector3.Dot(vector, normal) / normal.sqrMagnitude;

        return mirroredVector;
    }
}
