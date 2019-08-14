using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CircleMove : MonoBehaviour {
    float localGravity;
    float vx;
    float vy;

    void Start () {
        //give initial speed at fish's pointing direction
        int velocity = UnityEngine.Random.Range(12, 18);
        //get fish rotation angle
        float rotationX = GameObject.Find("afish").transform.eulerAngles.z;
        //interpolate velocity to x and y axis
        vx = Mathf.Cos(Mathf.Deg2Rad * rotationX) * velocity;
        vy = Mathf.Sin(Mathf.Deg2Rad * rotationX) * velocity;
    }
	
	// Update is called once per frame
	void FixedUpdate() {

        //collide with fish bowel right side
        foreach (Segment segment in rightpartcurve.rightcurve)
        {
            Vector3 p1 = segment.point1;
            Vector3 p2 = segment.point2;
            // calculalte shortest distance from circle to curve segment
            float distance = CalculateD(p1, p2, gameObject.transform.position);

            //since segment is really small can treat segemnet as a circle, circle collide with droplet response diretion is from curveCircle to droplet
            Vector3 responseDirection = new Vector3(gameObject.transform.position.x - (p1.x + p2.x) / 2f, gameObject.transform.position.y - (p1.y + p2.y) / 2f, 0);
            //normalize responddriection 
            responseDirection.Normalize();
            //calculate the magnitude of current velocity of droplet
            float magnitudeofV = (float)Math.Sqrt(vx * vx + vy * vy);
            //cheack if droplet is on the leftside of rightcurve of fish bowl if yes check is positive
            float check = CheckBugPosition(p1, p2, gameObject.transform.position);
            //check the previous droplet position
            Vector3 prePosition = new Vector3(gameObject.transform.position.x - vx * 0.02f, gameObject.transform.position.y - vy * 0.02f, 0);
            float check2 = CheckBugPosition(p1, p2, prePosition);

            //if droplet is on the leftside of the rightcurve of fish bowl
            if (check >= 0 || check2 > 0)
            {
                //if the distance between droplet and curve segment is small
                if (distance < gameObject.transform.localScale.x)
                {
                    //if droplet was on the left currently on the right of the curve
                    if (check2 >= 0 && check < 0)
                    {
                        //reverse response direction
                        responseDirection = -responseDirection;
                    }
                    //coefficient of restitution is 0.95 so loss little energy
                    //apply response force with some energy reduce
                    vx = 0.95f * responseDirection.x * magnitudeofV;
                    vy = 0.95f * responseDirection.x * magnitudeofV;
                    break;
                }
            }
            else {
                if (distance < gameObject.transform.localScale.x)
                {
                    vx = 0.95f * responseDirection.x * magnitudeofV;
                    vy = 0.95f * responseDirection.x * magnitudeofV;
                    break;
                }
            }
        }

        //do the collision solver with leftpartcurve as previos
        foreach (Segment segment in leftpartcurve.leftcurve)
        {
            Vector3 p1 = segment.point1;
            Vector3 p2 = segment.point2;

            float distance = CalculateD(p1, p2, gameObject.transform.position);
            Vector3 responseDirection = new Vector3(gameObject.transform.position.x - (p1.x + p2.x) / 2f, gameObject.transform.position.y - (p1.y + p2.y) / 2f, 0);
            responseDirection.Normalize();
            float magnitudeofV = (float)Math.Sqrt(vx * vx + vy * vy);
            //cheack if circle is on the leftside of rightcurve of fish bowl if yes check is positive
            float check = CheckBugPosition(p1, p2, gameObject.transform.position);
            Vector3 prePosition = new Vector3(gameObject.transform.position.x - vx * 0.02f, gameObject.transform.position.y - vy * 0.02f, 0);
            float check2 = CheckBugPosition(p1, p2, prePosition);

            if (check >= 0 || check2 > 0)
            {
                if (distance < gameObject.transform.localScale.x)
                {
                    if (check2 >= 0 && check < 0)
                    {
                        responseDirection = -responseDirection;
                    }
                    vx = 0.95f * responseDirection.x * magnitudeofV;
                    vy = 0.95f * responseDirection.x * magnitudeofV;
                    break;
                }
            }
            else {
                if (distance < gameObject.transform.localScale.x)
                {
                    vx = 0.95f * responseDirection.x * magnitudeofV;
                    vy = 0.95f * responseDirection.x * magnitudeofV;
                    break;
                }
            }
        }

        //use gravity
        vy -= 0.15f;

        //apply velocity
        gameObject.transform.position += (new Vector3(vx, vy, 0) * Time.deltaTime);
        
        //if droplets out of scene destroy
        if (gameObject.transform.position.y > 9f || gameObject.transform.position.y < -9f || gameObject.transform.position.x > 17f) {
            Destroy(gameObject);
        }

        //encounter with waterlevel then disapper
        foreach (Vector3 curvepoint in waterlevel.positions)
        {
            //calculate distance between waterlevel points
            float distanceX = Math.Abs(gameObject.transform.position.x - curvepoint.x);
            float distanceY = Math.Abs(gameObject.transform.position.y - curvepoint.y);
            float distance1 = (float)Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
            //if distance is small and droplets is coming from abouve the waterlevel
            if (distance1 <= gameObject.transform.localScale.x && vy < 0)
            {
                Destroy(gameObject);
                break;
            }
        }
    }


    private float CheckBugPosition(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float val = (p2.x - p1.x) * (p3.y - p1.y) - (p3.x - p1.x) * (p2.y - p1.y);
        return val;
    }

    private float CalculateD(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float distanceX = Mathf.Abs(p3.x - (p1.x + p2.x) / 2f);
        float distanceY = Mathf.Abs(p3.y - (p1.y + p2.y) / 2f);
        float distance = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);
        return distance;
    }
}
