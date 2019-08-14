using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishtail : MonoBehaviour {

    public LineRenderer tail;
    public Transform point0, point1, point2, point3;

    private int pointcount = 50;
    private Vector3[] positions1 = new Vector3[51];

    // Use this for initialization
    void Start()
    {
        tail.positionCount = 51;
        DrawCurveLine();
    }

    // Update is called once per frame
    void Update()
    {
        DrawCurveLine();
    }

    private void DrawCurveLine()
    {
        for (int i = 0; i < pointcount + 1; i++)
        {
            float t = (float)i / (float)pointcount;
            positions1[i] = beziercurve.CubicCurve(t, point0.position, point1.position, point2.position, point3.position);
        }
        tail.SetPositions(positions1);
    }

}
