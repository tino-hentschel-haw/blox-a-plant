using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Orbx_Rigidbody : MonoBehaviour
{
    public Collider col;
    public Color orbColor;
    public Color bloxColor;

    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        Debug.Log("plantZone entered");
    }

    void OnCollisionEnter(Collision collision) {
        
        
        if (collision.collider.tag == "blox")
        {
            orbColor = GetComponent<Renderer>().material.color;
            collision.transform.gameObject.GetComponent<Renderer>().material.color = orbColor;
            Debug.Log("Blox detected!");
        }

        if (collision.collider.name == "Terrain") {
            Destroy(gameObject, 2);
        }

    }
}