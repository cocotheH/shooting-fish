using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterlevel : MonoBehaviour {

    public LineRenderer water_level;
    public Transform point;

    private int pointcount = 500;
    public static Vector3[] positions = new Vector3[501];
    // Use this for initialization
    void Start()
    {
        float number = Random.Range(0.0f, 1.0f);
        water_level.positionCount = 501;
        DrawLine(number);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DrawLine(float number)
    {
        for (int i = 0; i < pointcount + 1; i++)
        {
            float t = (float)i / pointcount;
            float helper = number + (float)0.05*i;
            positions[i] = NextLinePosition(t, point.position,helper);
        }
        water_level.SetPositions(positions);
    }


    private Vector3 NextLinePosition(float t, Vector3 p0,float helper)
    {
        Vector3 p = new Vector3();
        p.x = p0.x + t* (float)pointcount / 10f* (float)0.316;
        p.y =Mathf.PerlinNoise(helper, helper) +(-4);
        return p;
    }
}
