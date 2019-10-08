using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public int speed;
    public int visionRange;

    public int angle;
    public int fov;

    private int radius = 1;

    public Vector3[] circleCoords;
  

    // Start is called before the first frame update
    void Start() {

        circleCoords = new Vector3[100];
        circleCoords = createPoints();
        
    }
    // Update is called once per frame
    void Update() {

        //Vector3 castDirection = transform.TransformDirection(Vector3.forward);
        //if (Physics.Raycast(transform.position, castDirection, visionRange)) {
        //    Debug.DrawRay(transform.position, castDirection * visionRange, Color.red);
        //} else {
        //    Debug.DrawRay(transform.position, castDirection * visionRange, Color.green);
        //}

        for (int i = 0; i < 100; i++) {
            Debug.DrawRay(transform.position, circleCoords[i] * visionRange, Color.blue);
        }
        
    }

    Vector3[] createPoints() {
        int raysXPlane = (int)(fov / angle);
        int raysPerRotation = (int)(360 / angle);

        circleCoords[0] = transform.TransformDirection(Vector3.forward);

        for (int i = 0; i < raysXPlane; i++) {
            circleCoords[i * raysPerRotation + 1] = Quaternion.AngleAxis(angle * (i + 1), gameObject.transform.up) * circleCoords[0];

            for (int j = 0; j < raysPerRotation; j++) {
                circleCoords[i * raysPerRotation + j + 2] = Quaternion.AngleAxis(angle, gameObject.transform.forward) * circleCoords[i * raysPerRotation + j + 1];
            }

        }

        return circleCoords;
    }
}
