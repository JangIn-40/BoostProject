using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] public float mainThrust = 1f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine = null;

    [SerializeField] ParticleSystem rightThrusterParticle = null;
    [SerializeField] ParticleSystem leftThrusterParticle = null;
    [SerializeField] ParticleSystem boosterParticle = null;
    

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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            LeftRotating();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RightRotating();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!boosterParticle.isPlaying)
        {
            boosterParticle.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        boosterParticle.Stop();
    }

    void LeftRotating()
    {
        ApplyRotation(Vector3.forward);
        if (!leftThrusterParticle.isPlaying)
            leftThrusterParticle.Play();
    }

    void RightRotating()
    {
        ApplyRotation(Vector3.back);
        if (!rightThrusterParticle.isPlaying)
            rightThrusterParticle.Play();
    }

    void StopRotating()
    {
        leftThrusterParticle.Stop();
        rightThrusterParticle.Stop();
    }

    void ApplyRotation(Vector3 vector3)
    {
        rb.freezeRotation = true;
        //강의에서는 float형을 매개변수로 받아서 rotationThrust를 바꿈 강의 방식이 데이터크기따져보면 더 효율적일듯
        transform.Rotate(vector3 * rotationThrust * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
