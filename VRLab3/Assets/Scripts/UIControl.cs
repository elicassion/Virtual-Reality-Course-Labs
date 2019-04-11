using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour {

    public GameObject XZNav;
    public GameObject XZRot;
    public GameObject YNav;

    public GameObject BuildModeBtn;
    public GameObject CarBtn;
    public GameObject HouseBtn;
    public GameObject AirplaneBtn;
    public GameObject HotairballoonBtn;
    public GameObject SaveCurrentObjBtn;
    public GameObject ExitBuildModeBtn;

    public GameObject CarObj;
    public GameObject HouseObj;
    public GameObject AirplaneObj;
    public GameObject HotairballoonObj;

    public bool isBuildMode;

    SimpleTouchController XZNavController;
    SimpleTouchController XZRotController;
    SimpleTouchController YNavController;

    public bool isBuildingObj;
    GameObject currentObj;
    string curObjType;

    float UIControlRate = 0.005f;

    

    

	// Use this for initialization
	void Start () {
        //BuildModeBtn = GameObject.Find("BuildModeBtn");
        //CarBtn = GameObject.Find("CarBtn");
        //CarObj = GameObject.Find("CarObj");
        CarBtn.SetActive(false);
        HouseBtn.SetActive(false);
        AirplaneBtn.SetActive(false);
        HotairballoonBtn.SetActive(false);
        ExitBuildModeBtn.SetActive(false);
        SaveCurrentObjBtn.SetActive(false);
        isBuildMode = false;
        isBuildingObj = false;

        XZNavController = XZNav.GetComponent<SimpleTouchController>();
        XZRotController = XZRot.GetComponent<SimpleTouchController>();
        YNavController = YNav.GetComponent<SimpleTouchController>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 XZNavVec = XZNavController.GetTouchPosition;
        Vector2 XZRotVec = XZRotController.GetTouchPosition;
        Vector2 YNavVec = YNavController.GetTouchPosition;
        if (isBuildMode)
        {
            if (isBuildingObj)
            {
                if (curObjType == "Air")
                {
                    currentObj.transform.Translate(new Vector3(XZNavVec.x, YNavVec.x, XZNavVec.y) * UIControlRate);
                    currentObj.transform.Rotate(new Vector3(XZRotVec.x, YNavVec.x, XZRotVec.y));
                }
                else if (curObjType == "Ground")
                {
                    currentObj.transform.Translate(new Vector3(XZNavVec.x, 0, XZNavVec.y) * UIControlRate);
                    currentObj.transform.Rotate(Vector3.up * XZRotVec.x);
                }
            }
            
        }
    }

    public void BuildModeBtnClick()
    {
        isBuildMode = true;
        BuildModeBtn.SetActive(false);
        showObjButtons();
        ExitBuildModeBtn.SetActive(true);
    }
    
    public void CarBtnClick()
    {
        isBuildingObj = true;
        CarObj.SetActive(true);
        currentObj = CarObj;
        curObjType = "Ground";
        toggleObjButtons("CAR");
    }

    public void HouseBtnClick()
    {
        isBuildingObj = true;
        HouseObj.SetActive(true);
        currentObj = HouseObj;
        curObjType = "Ground";
        toggleObjButtons("HOUSE");
    }

    public void AirplaneBtnClick()
    {
        isBuildingObj = true;
        AirplaneObj.SetActive(true);
        currentObj = AirplaneObj;
        curObjType = "Air";
        toggleObjButtons("AIRPLANE");
    }

    public void HotairballoonBtnClick()
    {
        isBuildingObj = true;
        HotairballoonObj.SetActive(true);
        currentObj = HotairballoonObj;
        curObjType = "Air";
        toggleObjButtons("HOTAIRBALLOON");
    }

    void toggleObjButtons(string objName)
    {
        bool[] status = new bool[4] {false, false, false, false};
        switch (objName)
        {
            case "CAR":
                status[0] = true;
                break;
            case "HOUSE":
                status[1] = true;
                break;
            case "AIRPLANE":
                status[2] = true;
                break;
            case "HOTAIRBALLOON":
                status[3] = true;
                break;
            default:
                break;
        }
        CarBtn.SetActive(status[0]);
        HouseBtn.SetActive(status[1]);
        AirplaneBtn.SetActive(status[2]);
        HotairballoonBtn.SetActive(status[3]);
        SaveCurrentObjBtn.SetActive(true);
    }

    void showObjButtons()
    {
        CarBtn.SetActive(true);
        HouseBtn.SetActive(true);
        AirplaneBtn.SetActive(true);
        HotairballoonBtn.SetActive(true);
    }

    void hideObjButtons()
    {
        CarBtn.SetActive(false);
        HouseBtn.SetActive(false);
        AirplaneBtn.SetActive(false);
        HotairballoonBtn.SetActive(false);
    }

    public void SaveCurrentObjClick()
    {
        isBuildingObj = false;
        currentObj = null;
        curObjType = "NULL";
        showObjButtons();
        SaveCurrentObjBtn.SetActive(false);
    }

    public void ExitBuildModeBtnClick()
    {
        isBuildMode = false;
        BuildModeBtn.SetActive(true);
        hideObjButtons();
        ExitBuildModeBtn.SetActive(false);
    }

}
