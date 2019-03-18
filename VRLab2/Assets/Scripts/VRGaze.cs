using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRGaze : MonoBehaviour {
    Vector3 menuDisFromCamera = Vector3.forward * 1f;
    public Image imgGaze;
    public float totalTime = 0.5f;
    bool gvrStatus;
    bool guiButtonStatus;
    float gvrTimer;

    public int distanceOfRay = 10;
    private RaycastHit _hit;

    public GameObject curItemsUIList;
    Dictionary<string, int> curItems = new Dictionary<string, int>();
    Dictionary<string, int> shoppingList = new Dictionary<string, int>();
    bool isItemsListShow = false;

    Transform curHit;
    GameObject curObj;
    Vector3 curObjOriScale;
    Color curObjColor;

    int trackingState = 0;
    // 0 shopping
    // 1 select item
    // 2 shopping cart&list menu

    [SerializeField] Transform UIPanel;
    public GameObject buttonList;
    public GameObject buttonPrefab;
    Text shoplistTextObj;

	// Use this for initialization
	void Start () {
        curObj = null;
        UIPanel.gameObject.SetActive(false);
        shoppingList.Add("apple", 2);
        shoppingList.Add("banana", 1);
        shoppingList.Add("beer", 1);
    }
	
	// Update is called once per frame
	void Update () {
        if (guiButtonStatus)
        {
            gvrTimer += Time.deltaTime;
            imgGaze.fillAmount = gvrTimer / totalTime;
            if (imgGaze.fillAmount == 1)
            {
                hideItemsInCart();
                guiButtonStatus = false;
                gvrTimer = 0;
                imgGaze.fillAmount = 0;
            }
        }
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out _hit, distanceOfRay))
        {
            //Debug.Log("[ITEM TAG]"+_hit.transform.tag);
            //Debug.Log("[ITEM Name]"+_hit.transform.gameObject.name);
            if (_hit.transform.tag.StartsWith("Item"))
            {
                string itemName = _hit.transform.tag.Split('_')[1];
                //Debug.Log("[Item Name] " + name);
                // pick/return item
                if (Input.GetKey(KeyCode.B))
                {
                    curObj.GetComponent<Teleport>().ToggleCart(curItems, itemName);
                    // This re-scale may not be needed
                    curObj.transform.localScale = curObjOriScale;
                }
            }
            if (_hit.transform.tag.StartsWith("HideList"))
            {
                if (imgGaze.fillAmount == 1)
                {
                    hideItemsInCart();
                }
            }
            /*else
            {
                //Debug.Log(_hit.transform.tag);
            }*/
        }
        // pop-up shopping cart && shopping list
        // (gaze on cart yields cart
        // other for shopping list -- not now)
        if (Input.GetKey(KeyCode.A))
        {
            showItemsInCart();
        }
    }

    public void GVROn()
    {
        gvrStatus = true;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //Debug.Log("GVR ON");
        if (Physics.Raycast(ray, out _hit, distanceOfRay))
        {
            //Debug.Log(_hit.transform.tag);
            curHit = _hit.transform;
            curObj = curHit.gameObject;
            curObjColor = curObj.GetComponent<MeshRenderer>().material.color;
            if (curHit.tag.StartsWith("Item"))
            {
                curObjOriScale = curObj.transform.localScale;
                curObj.transform.localScale *= 1.2f;
                curObj.GetComponent<MeshRenderer>().material.color = Color.blue;
            }
            //if (curHit.)
        }
        
    }

    public void GVROff()
    {
        gvrStatus = false;
        gvrTimer = 0;
        imgGaze.fillAmount = 0;
        if (curHit && curHit.tag.StartsWith("Item"))
        {
            curObj.transform.localScale = curObjOriScale;
            curObj.GetComponent<MeshRenderer>().material.color = curObjColor;
        }
        /*if (curHit.CompareTag("ShoppingCart"))
        {
            hideItemsInCart();
        }*/
        //curObj = null;
        //curHit = null;
    }



    /************Shopping List***********************/
    public void GUIButtionEnter()
    {
        guiButtonStatus = true;
    }

    public void GUIButtionExit()
    {
        guiButtonStatus = false;
        gvrTimer = 0;
        imgGaze.fillAmount = 0;
    }
    public void toggleItemsInCart()
    {
        if (!isItemsListShow)
        {
            showItemsInCart();
        }
        else
        {
            hideItemsInCart();
        }
    }

    public void hideItemsInCart()
    {
        UIPanel.gameObject.SetActive(false);
        isItemsListShow = false;
        
    }

    public void showItemsInCart()
    {
        UIPanel.gameObject.SetActive(true);
        UIPanel.transform.position = transform.position + menuDisFromCamera;
        //UIPanel.LookAt(-transform.position);
        isItemsListShow = true;
        Text shopListText = GameObject.Find("ShopList").GetComponent<Text>();
        string listStr = "";
        foreach (KeyValuePair<string, int> kv in shoppingList){
            string itemName = kv.Key;
            int requiredNum = kv.Value;
            //Debug.Log("[ShoppingList] " + itemName);
            int curNum;
            if (curItems.ContainsKey(itemName))
            {
                curNum = curItems[itemName];
            }
            else curNum = 0;

            //string btnText = String.Format("itemName ({0}/{1})", curNum, requiredNum);
            //MakeButton(btnText);
            listStr += String.Format("{0} ({1}/{2})\n\n", itemName, curNum, requiredNum);
        }
        shopListText.text = listStr;
    }

    public void MakeButton(string btnText)
    {
        GameObject btn = (GameObject)Instantiate(buttonPrefab);
        btn.transform.position = UIPanel.transform.position;
        btn.GetComponent<RectTransform>().SetParent(UIPanel.transform);
        btn.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 10);
        btn.layer = 5;
        Text textObj = btn.AddComponent<Text>();
        textObj.text = btnText;
        textObj.fontSize = 14;
    }
}
