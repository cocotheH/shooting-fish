using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beziercurve {

    public static Vector3 LinearCurve(float t, Vector3 p0, Vector3 p1)
    {
        return p0 + t * (p1 - p0);
    }

    public static Vector3 CubicCurve(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        Vector3 p = u * u * u * p0 + 3 * u * u * t * p1 + 3 * u * t * t * p2 + t * t * t * p3;
        return p;
    }

    public static Vector3 QuadraticCurve(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        Vector3 p = u * u * p0 + 2 * u * t * p1 + t * t * p2;
        return p;
    }


}
