using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fisheye : MonoBehaviour {
    public LineRenderer bottom;
    public Transform point0, point1;

    private int pointcount = 2;
    private Vector3[] positions = new Vector3[3];
    // Use this for initialization
    void Start()
    {

        bottom.positionCount = 3;
        DrawLine();
    }

    // Update is called once per frame
    void Update()
    {
        DrawLine();
    }

    private void DrawLine()
    {
        for (int i = 0; i < pointcount + 1; i++)
        {
            float t = (float)i / (float)pointcount;
            positions[i] = beziercurve.LinearCurve(t, point0.position, point1.position);
        }
        bottom.SetPositions(positions);
    }
}
