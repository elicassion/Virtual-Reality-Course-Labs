using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour {
    float speed = 1.0f;

    GameObject mwCamera;

	// Use this for initialization
	void Start () {
        mwCamera = GameObject.Find("Miniworld Camera");
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Joystick1Button3))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Joystick1Button2))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        
        mwCamera.transform.position = transform.position + new Vector3(0, 10, 0);
        mwCamera.transform.eulerAngles = new Vector3(90, 0, 0);
    }
}
