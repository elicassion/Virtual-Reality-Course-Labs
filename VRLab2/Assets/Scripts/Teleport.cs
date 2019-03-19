using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public GameObject shoppingCart;

    bool isInCart = false;
    Vector3 initPosOnShelf;
    LineRenderer pathLine;

    public void showProximityPath()
    {
        Vector3 curPos = transform.position;
        Vector3 anotherPos;
        if (isInCart)
        {
            anotherPos = initPosOnShelf;
        }
        else
        {
            anotherPos = shoppingCart.transform.position;
        }
        float dist = Vector3.Distance(curPos, anotherPos);
        if (dist < 10)
        {
            pathLine.SetPositions(new Vector3[2] { curPos, anotherPos});
        }
    }

    public void ToggleCart(Dictionary<string, int> curItems, string itemName)
    {
        if (isInCart)
        {
            removeCart();
            curItems[itemName] -= 1;
            if (curItems[itemName] == 0)
            {
                curItems.Remove(itemName);
            }
        } else
        {
            AddToCart();
            if (!curItems.ContainsKey(itemName))
            {
                curItems.Add(itemName, 0);
            }
            curItems[itemName] += 1;
        }
    }

    public void moveWithCart(Vector3 vec)
    {
        transform.Translate(vec);
    }

    public void removeCart()
    {
        transform.position = initPosOnShelf;
        isInCart = false;
    }

    public void AddToCart()
    {
        transform.position = new Vector3(shoppingCart.transform.position.x,
            shoppingCart.transform.position.y + 0.2f,
            shoppingCart.transform.position.z);
        isInCart = true;
    }

	// Use this for initialization
	void Start () {
        initPosOnShelf = transform.position;
        isInCart = false;
        pathLine = GameObject.Find("PathLine").GetComponent<LineRenderer>();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
