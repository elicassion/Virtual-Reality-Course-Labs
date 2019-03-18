using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMotion : MonoBehaviour {
    float moveSpeed = 10.0f;
    float turnSpeed = 50.0f;
    float timeTotal = 20.0f;
    Vector3 pos;
    Quaternion rot;

    // Use this for initialization
    void Start () {
        pos = transform.position;
        rot = transform.rotation;
    }

    
    void Update()
    {
        if (timeTotal < 0)
        {
            timeTotal = 20.0f;
            transform.position = pos;
            transform.rotation = rot;
        }
        timeTotal -= Time.deltaTime*3;
        
        if (timeTotal > 15.0f)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else if (timeTotal > 10.0f)
        {
            //transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            transform.Rotate(Vector3.forward, turnSpeed * Time.deltaTime);
        }
        else if (timeTotal > 5.0f)
        {
            transform.Translate(-Vector3.up * moveSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }
        else
        {
            //transform.Translate(-Vector3.up * moveSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            transform.Rotate(Vector3.right, turnSpeed * Time.deltaTime);
        }
        

    }
}
