using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public Transform balloonTransform;
    public Transform carTransform;
    public Transform buildingTransform;
    public Transform planeTransform;
    private int trackingMode = 0;
    private bool turned = false;
    private Vector3 lastCarCameraPos;
    private Vector3 lastCarCameraAngle;
    private Vector3 initCameraPos;
    private Vector3 initCameraAngle;
    enum Direction {
        Z_FORWORD, X_FORWORD, Z_BACKWORD, X_BACKWORD
    }
    private Direction carDirection = Direction.Z_FORWORD;
	// Use this for initialization
	void Start () {
        //lastCarCameraPos = carTransform;
        lastCarCameraPos = new Vector3(-12.46f, 1.3f, 6.81f);
        lastCarCameraAngle = new Vector3(0.0f, 0.0f, 0.0f);
        initCameraPos = transform.position;
        initCameraAngle = transform.eulerAngles;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.A))// A
        {
            
            if (trackingMode == 1)
            {
                /* On the second floor */
                trackingMode = 5;
                transform.position = new Vector3(7.7f, 11.5f, -6.4f);
            }
            else
            {
                /* On the first floor */
                trackingMode = 1;
                transform.position = new Vector3(7.7f, 1.6f, -6.4f);
            }
            
            //transform.position = planeTransform.position;
            //Application.Quit();

        }
        
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.B))// B
        {
            /* Scene: In The Car */
            trackingMode = 2;
            //transform.position = carTransform.position + new Vector3(-0.32f, 1.1f, -0.824839f);
            //transform.position = new Vector3(-12.46f, 1.2f, 6.81f);
            //transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            transform.position = lastCarCameraPos;
            transform.eulerAngles = lastCarCameraAngle;
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Y))// Y
        {
            /* Scene: In the hot air balloon */
            //Application.Quit();
            trackingMode = 3;
            transform.position = new Vector3(3.57f, 0.33f, -3.1f);
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.X))//X
        {
            
            //Application.Quit();
            trackingMode = 0;
            transform.position = initCameraPos;
            transform.eulerAngles = initCameraAngle;
        }

        Tracking();
        
    }

    void turnYClockwise90(Transform t)
    {
        t.Rotate(Vector3.up * 90);
        transform.Rotate(Vector3.up * 90);
    }

    void carMotion()
    {
        float carSpeed = 3.0f;
        switch (carDirection)
        {
            case Direction.Z_FORWORD:
                if (carTransform.position.z >= 15.1f)
                {
                    turnYClockwise90(carTransform);
                    carDirection = Direction.X_FORWORD;
                    break;
                }
                break;
            case Direction.X_FORWORD:
                if (carTransform.position.x >= 24.7f)
                {
                    turnYClockwise90(carTransform);
                    carDirection = Direction.Z_BACKWORD;
                    break;
                }
                break;
            case Direction.Z_BACKWORD:
                if (carTransform.position.z <= -24.8f)
                {
                    turnYClockwise90(carTransform);
                    carDirection = Direction.X_BACKWORD;
                    break;
                }
                break;
            case Direction.X_BACKWORD:
                if (carTransform.position.x <= -12.1f)
                {
                    turnYClockwise90(carTransform);
                    carDirection = Direction.Z_FORWORD;
                    break;
                }
                break;
        }
        carTransform.Translate(Vector3.forward * carSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * carSpeed * Time.deltaTime);
        lastCarCameraPos = transform.position;
        lastCarCameraAngle = transform.eulerAngles;
    }

    void hotAirBalloonMotion()
    {
        transform.position = balloonTransform.position + new Vector3(0, 2.49f, -0.52f);
        
    }

    void Tracking()
    {
        // Tracking
        if (trackingMode == 1)
        {
            //transform.position = planeTransform.position;
            //transform.LookAt(new Vector3(0, 0, 0));
        }

        else if (trackingMode == 2)
        {
            carMotion();
        }

        else if (trackingMode == 3)
        {
            hotAirBalloonMotion();
        }

    }
}
