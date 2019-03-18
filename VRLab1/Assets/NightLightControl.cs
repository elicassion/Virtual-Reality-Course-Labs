using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightLightControl : MonoBehaviour {
    public Light sunLt;
    Light lt;
    float initIntensity;
    // Use this for initialization
    void Start () {
        lt = GetComponent<Light>();
        initIntensity = lt.intensity;
    }
	
	// Update is called once per frame
	void Update () {
        if (sunLt.intensity < 0.5f)
        {
            lt.intensity = initIntensity;
        }
        else
        {
            lt.intensity = 0;
        }
    }
}
