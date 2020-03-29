using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

  
    public float speed  = 10f;
    public float Rotspeed = 90f;
    private Rigidbody MyRig ;

    //Joystick
    public Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        MyRig = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // For PC

        // float hAxis = Input.GetAxis("Horizontal");
        // float vAxis = Input.GetAxis("Vertical");

        //For Mobile

        float hAxis = joystick.Horizontal;
        float vAxis = joystick.Vertical;

       
        Vector3 dir = new Vector3(-vAxis,0,hAxis);

        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), Rotspeed *10* Time.deltaTime);
        }

        Vector3 movement = Vector3.forward * speed* Time.deltaTime;

        MyRig.MovePosition(transform.position +(transform.rotation *movement));
    }
}
