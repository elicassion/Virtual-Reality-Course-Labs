using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testjoystick : MonoBehaviour
{
    public float speed = 10.0F;
    public float rotationSpeed = 100.0F;
    void Update()
    {
        detectPressedKeyOrButton();
    }
    public void detectPressedKeyOrButton()
    {
        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
                Debug.Log("KeyCode down: " + kcode);
        }
    }
}
