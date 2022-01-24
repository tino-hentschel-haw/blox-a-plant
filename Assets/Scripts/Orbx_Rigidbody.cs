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
    
    private void OnTriggerEnter(Collider col)
    {
        // gameObject.GetComponent<Rigidbody>().useGravity = false;
        // Debug.Log("plantZone entered");
        // if (!col.gameObject.CompareTag("blox"))
        //     
        //
        var BloxColor = col.gameObject.GetComponent<BloxColor>();
        if(!BloxColor)
            return;
        
        BloxColor.SetColor(GetComponent<Renderer>().material.color);

    }

    void OnCollisionEnter(Collision collision) {
        
        var BloxColor = collision.gameObject.GetComponent<BloxColor>();
        if(!BloxColor)
            return;
        
        BloxColor.SetColor(GetComponent<Renderer>().material.color);
        
        // if (collision.collider.CompareTag("blox"))
        // {
        //     orbColor = GetComponent<Renderer>().material.color;
        //     collision.transform.gameObject.GetComponent<Renderer>().material.color = orbColor;
        //     Debug.Log("Blox detected!");
        // }
        //
        // if (collision.collider.name == "Terrain") {
        //     Destroy(gameObject, 2);
        // }
    
    }
}