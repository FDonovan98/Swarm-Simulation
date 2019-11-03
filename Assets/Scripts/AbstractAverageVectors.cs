using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractAverageVectors : MonoBehaviour
{
    // Returns the average unit vector of a list of Vector3
    public static Vector3 AverageOfVector3List(List<Vector3> originalVectors)
    {
        Vector3 averageVector = new Vector3();
        for (int i = 0; i < originalVectors.Count; i++)
        {
            averageVector += originalVectors[i];
        }

        return averageVector;
    }
}
