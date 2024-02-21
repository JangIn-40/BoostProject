using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float Period;
    Vector3 startingPosition;


    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        float cycle = Time.time / Period; //시간에 따라 계속해서 증가함

        const float tau = Mathf.PI * 2; //6.238의 일정한 값
        float rawSinwave = Mathf.Sin(cycle * tau); // -1 ~ 1

        movementFactor = (rawSinwave + 1f) / 2f; // 0 ~ 1 
        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPosition + offset; 
    }
}
