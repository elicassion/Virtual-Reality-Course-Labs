﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Vuforia;

public class DeployStage : MonoBehaviour {
    public GameObject UIControlObj;

    public GameObject AnchorStage;
    private PositionalDeviceTracker _deviceTracker;
    private GameObject _previousAnchor;

    UIControl uic;

    public void Start()
    {
        if (AnchorStage == null)
        {
            Debug.Log("AnchorStage must be specified");
            return;
        }
        AnchorStage.SetActive(false);

        uic = UIControlObj.GetComponent<UIControl>();
    }

    public void Awake()
    {
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
    }

    public void OnDestroy()
    {
        VuforiaARController.Instance.UnregisterVuforiaStartedCallback(OnVuforiaStarted);
    }

    private void OnVuforiaStarted()
    {
        _deviceTracker = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();
    }

    public void OnInteractiveHitTest(HitTestResult result)
    {
        if (result == null || AnchorStage == null)
        {
            Debug.LogWarning("Hit test is invalid or AnchorStage not set");
            return;
        }

        if (UIControlObj.GetComponent<UIControl>().isBuildingPlane)
        {
            var anchor = _deviceTracker.CreatePlaneAnchor(Guid.NewGuid().ToString(), result);
            uic.SetPlaneAnchor(anchor);
        }

        

        /*if (anchor != null)
        {
            AnchorStage.transform.parent = anchor.transform;

            AnchorStage.transform.localPosition = Vector3.zero;

            AnchorStage.transform.localRotation = Quaternion.identity;

            AnchorStage.SetActive(true);
        }
        if (_previousAnchor != null)
        {
            Destroy(_previousAnchor);
        }
        _previousAnchor = anchor;*/
    }
}
