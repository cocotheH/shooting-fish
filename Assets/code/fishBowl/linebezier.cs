using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linebezier : MonoBehaviour {

    public LineRenderer bottom;
    public Transform point0, point1;

    private int pointcount=50;
    private Vector3[] positions = new Vector3[51];
    // Use this for initialization
    void Start () {

        bottom.positionCount = 51;
        DrawLine();
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void DrawLine()
    {
        for (int i = 0; i < pointcount + 1; i++)
        {
            float t = (float)i / (float)pointcount;
            positions[i] = NextLinePosition(t, point0.position, point1.position);
        }
        bottom.SetPositions(positions);
    }


    private Vector3 NextLinePosition(float t, Vector3 p0, Vector3 p1)
    {
        return p0 + t * (p1 - p0);
    }

}
