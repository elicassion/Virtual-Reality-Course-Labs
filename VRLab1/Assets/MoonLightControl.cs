using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonLightControl : MonoBehaviour {
    public Light sunLt;
    Light moonLt;
    const float moonLightIntensity = 0.3f;
	// Use this for initialization
	void Start () {
        moonLt = GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update () {
        if (sunLt.intensity < 0.3f)
        {
            moonLt.intensity = moonLightIntensity - sunLt.intensity;
            RenderSettings.skybox.SetFloat("_Exposure", moonLightIntensity);
        }
        else
        {
            moonLt.intensity = 0;
            RenderSettings.skybox.SetFloat("_Exposure", sunLt.intensity);
        }
    }
}
