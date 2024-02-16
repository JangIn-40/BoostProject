using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("press space - thrusting");
            rb.AddRelativeForce(0, 1, 0);
        }

    }

    
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            Debug.Log("press leftarrow ");
        }
        else if(Input.GetKey(KeyCode.D))
        {
            Debug.Log("press rightarrow");
        }

    }
}
