using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishlower : MonoBehaviour {
    public LineRenderer fishLower;
    public Transform point0, point1, point2;

    private int pointcount = 50;
    private Vector3[] positions = new Vector3[51];
    // Use this for initialization
    void Start()
    {
        fishLower.positionCount = 51;
        DrawLine();
    }

    private void DrawLine()
    {
        for (int i = 0; i < pointcount + 1; i++)
        {
            float t = (float)i / (float)pointcount;
            positions[i] = beziercurve.QuadraticCurve(t, point0.position, point1.position, point2.position);
        }
        fishLower.SetPositions(positions);
    }

    // Update is called once per frame
    void Update () {
        DrawLine();
    }
}
