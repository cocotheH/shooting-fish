using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFish : MonoBehaviour {
    private float speed = 4f;
   //private CharacterController fish;
    private int flag =0;

    float moveLR;
    float rotx = 0;
    //float roty;


    // Use this for initialization
    void Start()
    {
        //fish = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        rotx = Input.GetAxis("Vertical");
        moveLR = Input.GetAxis("Horizontal") * speed;
        if (Input.GetKeyDown(KeyCode.W))
        {
            flag = 0;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            flag = 1;
        }
        if (gameObject.transform.position.x >= 6.4f) {
            float y = gameObject.transform.position.y;
            gameObject.transform.position = new Vector3(6.4f, y, 0);
        }
        if (gameObject.transform.position.x <= -6.15f)
        {
            float y = gameObject.transform.position.y;
            gameObject.transform.position = new Vector3(-6.15f, y, 0);
        }

        if (flag == 0 && gameObject.transform.eulerAngles.z >= 60f && gameObject.transform.eulerAngles.z < 298f) {
                gameObject.transform.eulerAngles = new Vector3(0, 0, 60f);
        }

        if (flag == 1 && gameObject.transform.eulerAngles.z <= 300f && gameObject.transform.eulerAngles.z > 62f) {
                gameObject.transform.eulerAngles = new Vector3(0, 0, 300f);
        }

        Vector3 movement = new Vector3(moveLR, 0, 0);
        transform.Translate(movement * Time.deltaTime, Space.World);
        transform.Rotate(0, 0, rotx);
    }

}
