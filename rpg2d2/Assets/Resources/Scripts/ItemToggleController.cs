using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemToggleController : MonoBehaviour {

    public int itemNo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Select()
    {
        GameObject.Find("Description").GetComponentInChildren<Text>().text = "【" + ItemList.item_table[itemNo].item_name + "】\n" + ItemList.item_table[itemNo].item_desc;
    }
}
