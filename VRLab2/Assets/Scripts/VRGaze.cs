using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRGaze : MonoBehaviour {
    public Image imgGaze;
    public float totalTime = 2;
    bool gvrStatus;
    float gvrTimer;

    public int distanceOfRay = 10;
    private RaycastHit _hit;

    GameObject curObj;

	// Use this for initialization
	void Start () {
        curObj = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (gvrStatus)
        {
            gvrTimer += Time.deltaTime;
            imgGaze.fillAmount = gvrTimer / totalTime;
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        
        if (Physics.Raycast(ray, out _hit, distanceOfRay))
        {
            if (_hit.transform.CompareTag("Item"))
            {
                curObj = _hit.transform.gameObject;
                curObj.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                if (imgGaze.fillAmount == 1)
                {
                    curObj.transform.localScale = new Vector3(1, 1, 1);
                    curObj.GetComponent<Teleport>().ToggleCart();
                }
            }
            
        }
	}

    public void GVROn()
    {
        gvrStatus = true;
    }

    public void GVROff()
    {
        gvrStatus = false;
        gvrTimer = 0;
        imgGaze.fillAmount = 0;
        curObj.transform.localScale = new Vector3(1, 1, 1);
        curObj = null;
    }
}
