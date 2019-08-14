using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Fish;
    public GameObject Bug;
    public GameObject Circles;

    private int count = 1;
    private int nextUpdate = 0;
    public static List<GameObject> bug = new List<GameObject>();
    // Use this for initialization
    void Start()
    {   
        //initialize fish
        GameObject fish = Instantiate(Fish, new Vector3(0f, -4.2f, 0f), Quaternion.identity) as GameObject;
        fish.name = "afish";
        //create first bug
        GameObject abug = Instantiate(Bug, new Vector3(-21f, 4.5f, 0f), Quaternion.identity) as GameObject;
        abug.name = "bug" + (count);
        bug.Add(abug);
        count++;
        int timerange = Random.Range(3, 8);
        nextUpdate = Mathf.FloorToInt(Time.time) + timerange;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // let fish be able to shoot droplets
        GenerateDroplets();

        //for every randomlized time generate a bug add bug to buglist
        int timerange = Random.Range(3, 5);
        if (Time.time >= nextUpdate)
        {
            GameObject abug = Instantiate(Bug, new Vector3(-21f, 4.5f, 0f), Quaternion.identity) as GameObject;
            abug.name = "bug" + (count);
            bug.Add(abug);
            nextUpdate = Mathf.FloorToInt(Time.time) + timerange;
            count++;
        }

        //if bug fly outof the scene destory it updat buglist
        if (bug != null)
        {
            for (int i = bug.Count - 1; i > -1; i--)
            {
                if (bug[i] != null && bug[i].transform.position.x > 21f)
                    Destroy(bug[i]);
                if (bug[i] == null) {
                    bug.RemoveAt(i);
                    count--;
                }

            }
        }
    }

    public void GenerateDroplets() {
        GameObject fish = GameObject.Find("afish");
        Vector3 position = fish.transform.position;

        // if space is pressed then generate 3 droplets
        if (Input.GetKeyDown("space")) {
            //radius 0.15 droplet
            position += new Vector3(1.0f, 0.5f, 0);
            GameObject droplet1 = Instantiate(Circles, position, Quaternion.identity) as GameObject;
            droplet1.name = "droplet1 ";
            droplet1.transform.localScale = new Vector3(0.15f,0.15f,0);
            //radius 0.25 droplet
            position += new Vector3(0.4f, 0.4f, 0);
            GameObject droplet2 = Instantiate(Circles, position, Quaternion.identity) as GameObject;
            droplet2.name = "droplet2 ";
            droplet2.transform.localScale = new Vector3(0.25f, 0.25f, 0);
            //radius 0.3 droplet
            position += new Vector3(0.4f, 0.45f, 0);
            GameObject droplet3 = Instantiate(Circles, position, Quaternion.identity) as GameObject;
            droplet3.name = "droplet3 ";
            droplet3.transform.localScale = new Vector3(0.3f, 0.3f, 0);

        }
    }
}