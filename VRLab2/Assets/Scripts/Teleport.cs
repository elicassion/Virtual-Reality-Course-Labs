using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public GameObject shoppingCart;

    bool isInCart = false;
    Vector3 initPosOnShelf;

    public void ToggleCart()
    {
        if (isInCart)
        {
            removeCart();
        } else
        {
            AddToCart();
        }
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
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
