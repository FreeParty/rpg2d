using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandItem : MonoBehaviour {

    public int item_id = 1;
    public bool isOpen = false;

    // Use this for initialization
    void Start () {
        gameObject.tag = "StrongBox";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Receive()
    {
        if (!isOpen)
        {
            LogController.logController.printText(new string[] { ItemList.ItemName(item_id) + "を手に入れた！" });
            PlayerContoroller.my_items.Add(item_id);
            isOpen = true;
        }
        else
        {

        }

    }
}
