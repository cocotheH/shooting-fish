using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class rightpartcurve : MonoBehaviour
{

    public LineRenderer right;
    public Transform point0, point2, point3, point4;

    private int pointcount = 1000;
    public static Vector3[] positions1 = new Vector3[1001];
    public static List<Segment> rightcurve = new List<Segment>();

    // Use this for initialization
    void Start()
    {
        right.positionCount = 1001;
        DrawRightCurveLine();
        GenerateSegments(positions1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DrawRightCurveLine()
    {
        for (int i = 0; i < pointcount + 1; i++)
        {
            float t = (float)i / (float)pointcount;
            positions1[i] = NextCurvePosition(t, point2.position, point3.position, point4.position, point0.position);
        }
        right.SetPositions(positions1);
    }


    private Vector3 NextCurvePosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        Vector3 p = u * u * u * p0 + 3 * u * u * t * p1 + 3 * u * t * t * p2 + t * t * t * p3;
        return p;
    }

    private void GenerateSegments(Vector3[] positions)
    {
        for (int i = pointcount; i >= 1; i = i - 1)
        {
            Segment segment = new Segment();
            segment.point1 = positions[i];
            segment.point2 = positions[i - 1];
            Vector2 normal;
            double p1x = segment.point1.x;
            double p1y = segment.point1.y;
            double p2x = segment.point2.x;
            double p2y = segment.point2.y;
            normal.x = (float)(-(p2y - p1y) /
                Math.Sqrt(((p2x - p1x) * (p2x - p1x))
                + (p2y - p1y) * (p2y - p1y)));
            normal.y = (float)((p2x - p1x) /
                Math.Sqrt(((p2x - p1x) * (p2x - p1x)
                + (p2y - p1y) * (p2y - p1y))));
            segment.normal = normal;
            rightcurve.Add(segment);
            //Debug.Log(segment.point1);
        }
    }
}
