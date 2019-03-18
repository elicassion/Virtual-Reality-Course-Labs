using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLightControl : MonoBehaviour {
    public Light sunLt;
    public Light carLightLeft;
    public Light carLightRight;
    private const float carLightIntensity = 5.79f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (sunLt.intensity < 0.5f)
        {
            carLightLeft.intensity = carLightIntensity;
            carLightRight.intensity = carLightIntensity;
        }
        else
        {
            carLightLeft.intensity = 0;
            carLightRight.intensity = 0;
        }
	}
}
