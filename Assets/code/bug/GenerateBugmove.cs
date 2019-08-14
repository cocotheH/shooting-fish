using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenerateBugmove : MonoBehaviour {
    private int nextUpdate = 0;
    public static float currentWindForce;
    private Vector3 totalforce = new Vector3();
    private GameObject[] droplets;
    private int flag = 1;
    // Use this for initialization
    void Start () {
        float windMin = 4f;
        float windMax = 16f;
        currentWindForce = windMin + (windMax - windMin) * UnityEngine.Random.Range(0.0f, 1.0f);
        totalforce = currentWindForce * Vector3.right;
    }
	
	// Update is called once per frame
	void FixedUpdate() {

        Vector3 windDirection = Vector3.right;

        // If the next update is reached
        if (Time.time >= nextUpdate)
        {

            // Change the next update (current second+1)
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;

            //generate random wind force
            float windMin = 4f;
            float windMax = 16f;
            currentWindForce = windMin + (windMax - windMin) * UnityEngine.Random.Range(0.0f, 1.0f);
        }

        //add windforce
        if (flag == 1) {
            totalforce = currentWindForce * windDirection;
        }

        //if droplets are higher than the bowl then add wind velocity
        droplets = GameObject.FindGameObjectsWithTag("bubble");
        foreach (GameObject droplet in droplets)
        {
            if (droplet.transform.position.y > 3.6f)
            {
                droplet.transform.position += currentWindForce * Time.deltaTime * windDirection;
            }
         }

        //when droplet hit bug bug start to falling
        foreach (GameObject droplet in droplets)
        {
            float distanceX = Math.Abs(gameObject.transform.position.x - droplet.transform.position.x);
            float distanceY = Math.Abs(gameObject.transform.position.y - droplet.transform.position.y);
            float distance = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);
            if (distance <= 0.5f + droplet.transform.localScale.x) {
                Destroy(droplet);
                flag = 0;
            }
        }


        //encounter with waterlevel then disapper
        foreach (Vector3 curvepoint in waterlevel.positions)
        {
            float distanceX = Math.Abs(gameObject.transform.position.x - curvepoint.x);
            float distanceY = Math.Abs(gameObject.transform.position.y - curvepoint.y);
            float distance = (float)Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
            if (distance <= 0.3f && gameObject.transform.position.y > curvepoint.y)
            {
                Destroy(gameObject);
                break;
            }
        }

        //collide with fish bowel
        foreach (Segment segment in rightpartcurve.rightcurve)
        {
            Vector3 p1 = segment.point1;
            Vector3 p2 = segment.point2;
            Vector3 responseDirection = new Vector3(gameObject.transform.position.x - (p1.x + p2.x) / 2f, gameObject.transform.position.y - (p1.y + p2.y) / 2f, 0);
            responseDirection.Normalize();
            float magnitudeofV = (float)Math.Sqrt(totalforce.x * totalforce.x + totalforce.y * totalforce.y);
            float distance = CalculateD(p1, p2, gameObject.transform.position);
            // check bug current position relative to curve
            float check = CheckBugPosition(p1, p2, gameObject.transform.position);
            // check bug previous frame  position relative to curve
            float check3 = CheckBugPosition(p1, p2, gameObject.transform.position - totalforce * 0.02f);
            if (check >= 0 || check3 >=0)
            {
                if (distance <= 0.5f)
                {
                    if (check3 >=0 && check < 0) {
                        responseDirection = -responseDirection;
                    }
                    totalforce = 0.5f * magnitudeofV * responseDirection;
                    break;
                }
            }
            else if (check < 0 ) {
                if (distance <= 0.5f)
                {
                    totalforce = 0.5f * magnitudeofV * responseDirection;
                    break;
                }
            }
        }

        foreach (Segment segment in leftpartcurve.leftcurve)
        {
            Vector3 p1 = segment.point1;
            Vector3 p2 = segment.point2;
            Vector3 responseDirection = new Vector3(gameObject.transform.position.x - (p1.x + p2.x) / 2f, gameObject.transform.position.y - (p1.y + p2.y) / 2f, 0);
            responseDirection.Normalize();
            float magnitudeofV = (float)Math.Sqrt(totalforce.x * totalforce.x + totalforce.y * totalforce.y);
            float distance = CalculateD(p1, p2, gameObject.transform.position);
            float check = CheckBugPosition(p1, p2, gameObject.transform.position);
            float check3 = CheckBugPosition(p1, p2, gameObject.transform.position - totalforce * 0.02f);
            if (check >= 0 || check3 >0)
            {
                if (distance <= 0.5f)
                {

                    if (check3 > 0 && check < 0)
                    {
                        responseDirection = -responseDirection;
                    }
                    totalforce = 0.6f * magnitudeofV * responseDirection;
                    //totalforce = totalforce - new Vector3(0, 0.5f, 0);
                    break;
                }
            }
            else if (check < 0)
            {
                if (distance <= 0.5f)
                {
                    totalforce = 0.6f * magnitudeofV * responseDirection;
                    break;
                }
            }
        }

        // add gravity to bug
        if (flag == 0)
        {
            totalforce = totalforce - new Vector3(0, 0.5f, 0);
        }

        //add total force to bug
        transform.position += totalforce * Time.deltaTime;
    }

    private float CheckBugPosition(Vector3 p1, Vector3 p2, Vector3 p3) {
        float val = (p2.x - p1.x) * (p3.y - p1.y) - (p3.x - p1.x) * (p2.y - p1.y);
        return val;
    }

    private float CalculateD(Vector3 p1, Vector3 p2, Vector3 p3) {
        float distanceX = Mathf.Abs(p3.x - (p1.x + p2.x) / 2f);
        float distanceY = Mathf.Abs(p3.y - (p1.y + p2.y) / 2f);
        float distance = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);
        return distance;
    }
}
