using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMotion : MonoBehaviour {
    enum Direction
    {
        Z_FORWARD, X_FORWARD, Z_BACKWARD, X_BACKWARD
    }
    private Direction carDirection = Direction.Z_FORWARD;

    bool isBuildMode = false;

    public GameObject UIController;

    // Use this for initialization
    void Start () {
        UIController = GameObject.Find("Camera UI");
        //transform.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (UIController.GetComponent<UIControl>().isBuildMode)
        {

        }
        else
        {
            carMotion();
        }
	}

    void carMotion()
    {
        float carSpeed = 0.02f;
        switch (carDirection)
        {
            case Direction.Z_FORWARD:
                if (transform.position.z >= 0.1f)
                {
                    turnYClockwise90(transform);
                    carDirection = Direction.X_BACKWARD;
                    break;
                }
                break;
            case Direction.X_FORWARD:
                if (transform.position.x >= 0.1f)
                {
                    turnYClockwise90(transform);
                    carDirection = Direction.Z_FORWARD;
                    break;
                }
                break;
            case Direction.Z_BACKWARD:
                if (transform.position.z <= -0.1f)
                {
                    turnYClockwise90(transform);
                    carDirection = Direction.X_FORWARD;
                    break;
                }
                break;
            case Direction.X_BACKWARD:
                if (transform.position.x <= -0.1f)
                {
                    turnYClockwise90(transform);
                    carDirection = Direction.Z_BACKWARD;
                    break;
                }
                break;
        }
        transform.Translate(Vector3.forward * carSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * carSpeed * Time.deltaTime);
    }

    void turnYClockwise90(Transform t)
    {
        t.Rotate(-Vector3.up * 90);
    }
}
