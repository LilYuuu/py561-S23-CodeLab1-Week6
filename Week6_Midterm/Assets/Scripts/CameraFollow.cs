using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    // public ThrowPlane throwPlaneScript;
    
    // Start is called before the first frame update
    void Start()
    {
        // throwPlaneScript = GetComponent<ThrowPlane>();
    }

    // Update is called once per frame
    void Update () {
        if (ThrowPlane.Instance.flying && player.transform.position.z is >=0 and < 175)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z);    
        }        
    }
}
