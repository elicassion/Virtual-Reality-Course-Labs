using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour {
    // 
    //
    //public float dayTime = 0.0f;
    float dayTime = 150.0f;
    Light sunLt;
    const float fullIntensity = 1.0f;
    const float baseIntensity = 0.005f;
    float angularSpeed = 180.0f / 600;
    Vector3 pos;
    // Use this for initialization
    void Start () {
        pos = transform.position;
        transform.eulerAngles = new Vector3(45, 45, 23);
        dayTime = 150.0f;
        sunLt = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        dayTime += Time.deltaTime * 10;
        if (dayTime >= 600.0f) {
            dayTime = 0.0f;
            transform.eulerAngles = new Vector3(0, 0, 23);
        }
        if (dayTime >= 0.0f && dayTime < 150.0f) {
            sunLt.intensity = (fullIntensity - baseIntensity) / 150.0f * dayTime + baseIntensity;
        }
        else if (dayTime >= 150.0f && dayTime < 300.0f)
        {
            sunLt.intensity = fullIntensity;
        }
        else if (dayTime >= 300.0f && dayTime < 450.0f)
        {
            sunLt.intensity = fullIntensity - (dayTime - 300.0f) * (fullIntensity - baseIntensity) / 150.0f;
        }
        else if (dayTime >= 450.0f)
        {
            sunLt.intensity = baseIntensity;
        }
        //RenderSettings.skybox.SetFloat("_Exposure", Mathf.Sin(Time.time * Mathf.Deg2Rad * 100) + 1);
        
        //this.angle = (this.angle - this.angularSpeed * Time.deltaTime*10);
        /*
        if (this.angle < -Mathf.PI) this.angle = Mathf.PI;
        float posX = this.lightDistance * Mathf.Cos(this.angle);
        float posY = this.lightDistance * Mathf.Sin(this.angle);
        float posZ = -this.lightDistance * Mathf.Sin(this.angle);
        transform.position = new Vector3(posX, posY, posZ);
        */
        transform.Rotate(Vector3.up * angularSpeed * Time.deltaTime * 10 +
            Vector3.right * angularSpeed * Time.deltaTime * 10);
    }
}
