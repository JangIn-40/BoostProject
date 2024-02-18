using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine = null;

    Rigidbody rb;
    AudioSource audioSource;

    public bool isAlive;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
        else
        {
            audioSource.Stop();
        }

    }

    
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(Vector3.forward);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(Vector3.back);
        }

    }

    void ApplyRotation(Vector3 vector3)
    {
        rb.freezeRotation = true;
        //강의에서는 float형을 매개변수로 받아서 rotationThrust를 바꿈 강의 방식이 데이터크기따져보면 더 효율적일듯
        transform.Rotate(vector3 * rotationThrust * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
