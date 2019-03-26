using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public GameObject shoppingCart;

    bool isInCart = false;
    Vector3 initPosOnShelf;
    LineRenderer pathLine;
    Transform oriParent;
    Vector3 oriScale;
    GameObject player;

    float xOffset = 0.5f;
    float zOffset = 0.7f;

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
        Debug.Log("[Item Name] " + itemName);
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
            int curItemsCount = countItemsInCart(curItems);
            AddToCart(curItemsCount);
            if (!curItems.ContainsKey(itemName))
            {
                curItems.Add(itemName, 0);
            }
            curItems[itemName] += 1;
        }
    }

    public int countItemsInCart(Dictionary<string, int> curItems)
    {
        int count = 0;
        foreach(KeyValuePair<string, int> kv in curItems)
        {
            count += kv.Value;
        }
        return count;
    }

    public void moveWithCart(Vector3 vec)
    {
        transform.Translate(vec);
    }

    public void removeCart()
    {
        transform.position = initPosOnShelf;
        transform.SetParent(oriParent);
        isInCart = false;
    }

    public void AddToCart(int curItemsCount)
    {
        float itemXOffset = curItemsCount / 4 * xOffset - 0.3f;
        float itemZOffset = curItemsCount % 4 * zOffset;
        transform.localScale = oriScale;
        transform.position = new Vector3(shoppingCart.transform.position.x+itemXOffset,
            shoppingCart.transform.position.y + 0.2f,
            shoppingCart.transform.position.z + itemZOffset);
        transform.SetParent(player.transform);
        isInCart = true;
    }

	// Use this for initialization
	void Start () {
        initPosOnShelf = transform.position;
        isInCart = false;
        pathLine = GameObject.Find("PathLine").GetComponent<LineRenderer>();
        oriParent = transform.parent;
        oriScale = transform.localScale;
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
