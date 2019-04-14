using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour {

    public GameObject ARPlane;
    public GameObject Plane1;
    public GameObject Plane2;

    public GameObject CarPrefab;
    public GameObject HousePrefab;
    public GameObject AirplanePrefab;
    public GameObject HotairballoonPrefab;

    public GameObject XZNav;
    public GameObject XZRot;
    public GameObject YNav;

    public GameObject BuildModeBtn;

    public GameObject BuildPlane1Btn;
    public GameObject BuildPlane2Btn;
    public GameObject ExitBuildModeBtn;


    public GameObject PlaneBtn;
    public GameObject CarBtn;
    public GameObject HouseBtn;
    public GameObject AirplaneBtn;
    public GameObject HotairballoonBtn;
    public GameObject RemovelastObjBtn;
    public GameObject BackToPlanesBtn;
    

    public GameObject SaveCurrentObjBtn;
    public GameObject DiscardCurrentObjBtn;
    public GameObject SaveCurrentPlaneBtn;
    

    //public GameObject CarObj;
    //public GameObject HouseObj;
    //public GameObject AirplaneObj;
    //public GameObject HotairballoonObj;
   

    public bool isBuildMode;

    SimpleTouchController XZNavController;
    SimpleTouchController XZRotController;
    SimpleTouchController YNavController;

    public bool isBuildingObj;
    GameObject currentObj;
    string curObjType;

    public bool isBuildingPlane;
    int buildingPlaneNum = -1;
    
    GameObject curPlane;
    GameObject prevAnchor;


    float UIControlRate = 0.005f;

    Stack<GameObject> PlacedObjs1;
    Stack<GameObject> PlacedObjs2;

	// Use this for initialization
	void Start () {
        PlacedObjs1 = new Stack<GameObject>();
        PlacedObjs2 = new Stack<GameObject>();


        //BuildModeBtn = GameObject.Find("BuildModeBtn");
        //CarBtn = GameObject.Find("CarBtn");
        //CarObj = GameObject.Find("CarObj");
        //CarObj.SetActive(false);
        //HouseObj.SetActive(false);
        //AirplaneObj.SetActive(false);
        //HotairballoonObj.SetActive(false);


        BuildPlane1Btn.SetActive(false);
        BuildPlane2Btn.SetActive(false);
        ExitBuildModeBtn.SetActive(false);

        PlaneBtn.SetActive(false);
        CarBtn.SetActive(false);
        HouseBtn.SetActive(false);
        AirplaneBtn.SetActive(false);
        HotairballoonBtn.SetActive(false);

        SaveCurrentPlaneBtn.SetActive(false);
        SaveCurrentObjBtn.SetActive(false);
        DiscardCurrentObjBtn.SetActive(false);
        RemovelastObjBtn.SetActive(false);
        BackToPlanesBtn.SetActive(false);

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
        showPlaneButtons();
        ExitBuildModeBtn.SetActive(true);
    }

    public void BuildPlane1BtnClick()
    {
        buildingPlaneNum = 0;
        curPlane = Plane1;
        showObjButtons();
        showObjEditButtons();
        hidePlaneButtons();
    }

    public void BuildPlane2BtnClick()
    {
        buildingPlaneNum = 1;
        curPlane = Plane2;
        showObjButtons();
        showObjEditButtons();
        hidePlaneButtons();
    }

    public void ExitBuildModeBtnClick()
    {
        isBuildMode = false;
        BuildModeBtn.SetActive(true);
        hidePlaneButtons();
        ExitBuildModeBtn.SetActive(false);
    }


    public void PlaneBtnClick()
    {
        isBuildingPlane = true;
        toggleObjButtons("PLANE");
        
    }

    public void CarBtnClick()
    {
        isBuildingObj = true;
        GameObject CarClone = GameObject.Instantiate(CarPrefab, new Vector3(0.2f, 0, -0.2f), Quaternion.identity, curPlane.transform) as GameObject;
        CarClone.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
        //CarClone.transform.parent = ARPlane.transform;
       // CarClone.SetActive(true);
        currentObj = CarClone;
        curObjType = "Ground";
        toggleObjButtons("CAR");
    }

    public void HouseBtnClick()
    {
        isBuildingObj = true;
        GameObject HouseClone = GameObject.Instantiate(HousePrefab, new Vector3(0, 0, 0), Quaternion.identity, curPlane.transform) as GameObject;
        //HouseClone.SetActive(true);
        currentObj = HouseClone;
        curObjType = "Ground";
        toggleObjButtons("HOUSE");
    }

    public void AirplaneBtnClick()
    {
        isBuildingObj = true;
        GameObject AirplaneClone = GameObject.Instantiate(AirplanePrefab, new Vector3(0, 0.7f, 0), Quaternion.identity, curPlane.transform) as GameObject;
        //AirplaneClone.SetActive(true);
        currentObj = AirplaneClone;
        curObjType = "Air";
        toggleObjButtons("AIRPLANE");
    }

    public void HotairballoonBtnClick()
    {
        isBuildingObj = true;
        GameObject HotairballoonClone = GameObject.Instantiate(HotairballoonPrefab, new Vector3(0, 0.3f, 0), Quaternion.identity, curPlane.transform) as GameObject;
        //HotairballoonClone.SetActive(true);
        currentObj = HotairballoonClone;
        curObjType = "Air";
        toggleObjButtons("HOTAIRBALLOON");
        
    }


    public void RemoveLastObjBtnClick()
    {
        GameObject g;
        if (buildingPlaneNum == 0)
        {
            g = PlacedObjs1.Pop();
        }
        else
        {
            g = PlacedObjs2.Pop();
        }
        Destroy(g);
    }

    public void BackToPlanesBtnClick()
    {
        hideObjButtons();
        BackToPlanesBtn.SetActive(false);
        SaveCurrentObjBtn.SetActive(false);
        showPlaneButtons();
        ExitBuildModeBtn.SetActive(true);
    }


    

    public void SaveCurrentObjBtnClick()
    {
        if (buildingPlaneNum == 0)
        {
            PlacedObjs1.Push(currentObj);
        }
        else
        {
            PlacedObjs2.Push(currentObj);
        }
        isBuildingObj = false;
        currentObj = null;
        curObjType = "NULL";
        showObjButtons();
        showObjEditButtons();
        hideThisObjEditButtons();
    }

    

    public void DiscardCurrentObjBtnClick()
    {
        isBuildingObj = false;
        currentObj = null;
        curObjType = "NULL";
        showObjButtons();
        hideThisObjEditButtons();
        showObjEditButtons();
    }



    public void DefinePlaneBtnClick()
    {
        isBuildingPlane = true;
    }

    public void SaveCurPlaneBtnClick()
    {
        isBuildingPlane = false;
        SaveCurrentPlaneBtn.SetActive(false);
        showObjButtons();
        BackToPlanesBtn.SetActive(true);
    }


    public void SetPlaneAnchor(GameObject anchor)
    {
        if (isBuildingPlane)
        {
            if (anchor != null)
            {
                curPlane.transform.parent = anchor.transform;
                curPlane.transform.localPosition = Vector3.zero;
                curPlane.transform.localRotation = Quaternion.identity;
                curPlane.SetActive(true);
            }
            if (prevAnchor != null)
            {
                Destroy(prevAnchor);
            }
            prevAnchor = anchor;
        }
    }

    public void DayNightModeToggle(bool dayActivated)
    {
        if (dayActivated)
        {

        }
        else
        {

        }
    }

    void showPlaneButtons()
    {
        BuildPlane1Btn.SetActive(true);
        BuildPlane2Btn.SetActive(true);
    }

    void hidePlaneButtons()
    {
        BuildPlane1Btn.SetActive(false);
        BuildPlane2Btn.SetActive(false);
    }


    void toggleObjButtons(string objName)
    {
        hideObjEditButtons();
        bool[] status = new bool[5] { false, false, false, false, false };
        switch (objName)
        {
            case "PLANE":
                status[0] = true;
                SaveCurrentPlaneBtn.SetActive(true);
                break;
            case "CAR":
                status[1] = true;
                showThisObjEditButtons();
                break;
            case "HOUSE":
                status[2] = true;
                showThisObjEditButtons();
                break;
            case "AIRPLANE":
                status[3] = true;
                showThisObjEditButtons();
                break;
            case "HOTAIRBALLOON":
                status[4] = true;
                showThisObjEditButtons();
                break;
            default:
                break;
        }
        PlaneBtn.SetActive(status[0]);
        CarBtn.SetActive(status[1]);
        HouseBtn.SetActive(status[2]);
        AirplaneBtn.SetActive(status[3]);
        HotairballoonBtn.SetActive(status[4]);
        BackToPlanesBtn.SetActive(false);
    }

    void showObjButtons()
    {
        PlaneBtn.SetActive(true);
        CarBtn.SetActive(true);
        HouseBtn.SetActive(true);
        AirplaneBtn.SetActive(true);
        HotairballoonBtn.SetActive(true);
    }

    void hideObjButtons()
    {
        PlaneBtn.SetActive(false);
        CarBtn.SetActive(false);
        HouseBtn.SetActive(false);
        AirplaneBtn.SetActive(false);
        HotairballoonBtn.SetActive(false);
    }

    void showObjEditButtons()
    {
        BackToPlanesBtn.SetActive(true);
        RemovelastObjBtn.SetActive(true);
        ExitBuildModeBtn.SetActive(false);
    }

    void hideObjEditButtons()
    {
        BackToPlanesBtn.SetActive(false);
        RemovelastObjBtn.SetActive(false);
        ExitBuildModeBtn.SetActive(true);
    }

    void showThisObjEditButtons()
    {
        DiscardCurrentObjBtn.SetActive(true);
        SaveCurrentObjBtn.SetActive(true);
    }

    void hideThisObjEditButtons()
    {
        DiscardCurrentObjBtn.SetActive(false);
        SaveCurrentObjBtn.SetActive(false);
    }
}
