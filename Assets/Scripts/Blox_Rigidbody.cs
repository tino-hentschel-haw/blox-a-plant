using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Blox_Rigidbody : MonoBehaviour
{
    public Collider col;

    void Start()
    {
      
    }
    private void OnTriggerEnter(Collider col)
    {
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        Debug.Log("plantZone entered");
    }
    
}
