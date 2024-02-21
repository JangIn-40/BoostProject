using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostItem : MonoBehaviour
{
    [SerializeField] float boost;

    Movement movement;

    void Start() 
    {
        movement = GetComponent<Movement>();
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Fuel"))
        {
            movement.mainThrust *= boost;
            Debug.Log(movement.mainThrust);
            Destroy(other.gameObject);
        }
    }
}
