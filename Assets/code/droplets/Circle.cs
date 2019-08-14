using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {

    static int radius=1;
    public int segments;
   // public Transform point0;
    public LineRenderer line;
    private Vector3[] positions = new Vector3[51];

    void Start()
    {
        line.positionCount = (segments + 1);
        //CreatePoints();
    }

    private void CreatePoints()
    {
        float x;
        float y;
        float z = 0f;

        float angle = 20f;
        
        
        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * gameObject.transform.localScale.x ;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * gameObject.transform.localScale.x;
            x += gameObject.transform.position.x;
            y += gameObject.transform.position.y;
            Vector3 p = new Vector3(x, y, z);
            positions[i] = p;
            angle += (360f / segments);
        }
        line.SetPositions(positions);
    }

    void Update()
    {
        CreatePoints();
    }

}
