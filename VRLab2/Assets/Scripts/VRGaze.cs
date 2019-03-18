using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRGaze : MonoBehaviour {
    public Image imgGaze;
    public float totalTime = 1;
    bool gvrStatus;
    float gvrTimer;

    public int distanceOfRay = 10;
    private RaycastHit _hit;

    public GameObject curItemsUIList;
    Dictionary<string, int> curItems;

    Transform curHit;
    GameObject curObj;
    Vector3 curObjOriScale;

    [SerializeField] Transform UIPanel;

	// Use this for initialization
	void Start () {
        curObj = null;
        UIPanel.gameObject.SetActive(false);

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
            //curObj = _hit.transform.gameObject;
            if (_hit.transform.CompareTag("Item"))
            {
                if (imgGaze.fillAmount == 1)
                {
                    // This re-scale may not be needed
                    //curObj.transform.localScale = curObjOriScale;
                    curObj.GetComponent<Teleport>().ToggleCart();
                }
            }
            if (_hit.transform.CompareTag("ShoppingCart"))
            {
                //curObj = _hit.transform.gameObject
                if (imgGaze.fillAmount == 1)
                {
                    showItemsInCart();
                }
            }
            else
            {
                //Debug.Log(_hit.transform.tag);
            }
        }
	}

    public void GVROn()
    {
        gvrStatus = true;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out _hit, distanceOfRay))
        {
            curHit = _hit.transform;
            curObj = curHit.gameObject;
            if (curHit.CompareTag("Item"))
            {
                curObjOriScale = curObj.transform.localScale;
                curObj.transform.localScale *= 1.2f;
            }
        }
    }

    public void GVROff()
    {
        gvrStatus = false;
        gvrTimer = 0;
        imgGaze.fillAmount = 0;
        if (curHit.CompareTag("Item"))
        {
            curObj.transform.localScale = curObjOriScale;
        }
        if (curHit.CompareTag("ShoppingCart"))
        {
            hideItemsInCart();
        }
        curObj = null;
    }

    public void hideItemsInCart()
    {
        UIPanel.gameObject.SetActive(false);
    }

    public void showItemsInCart()
    {
        UIPanel.gameObject.SetActive(true);
        //UIPanel.transform.position = new Vector3(0, 0, 0);
    }
}
