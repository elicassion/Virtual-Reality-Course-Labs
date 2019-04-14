﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotairballoonMotion : MonoBehaviour {

    float upSpeed = 10.0f * 0.01f;
    float forwardSpeed = 20.0f * 0.01f;
    Vector3 pos;
    Quaternion rot;
    float timeTotal = 0f;
    // Use this for initialization
    void Start()
    {
        pos = transform.position;
        rot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        timeTotal += Time.deltaTime * 3;

        if (timeTotal < 5.0f)
        {
            transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
        }
        else if (timeTotal < 15.0f)
        {
            transform.Translate(Vector3.up * upSpeed * 0.3f * Time.deltaTime + Vector3.forward * forwardSpeed * Time.deltaTime);
        }
        else
        {
            //transform.Translate(-Vector3.up * moveSpeed * Time.deltaTime);
            transform.Translate(-Vector3.up * upSpeed * Time.deltaTime);
            if (transform.position.y < pos.y)
            {
                timeTotal = 0.0f;
                transform.position = pos;
                transform.rotation = rot;
            }
        }
    }
}
