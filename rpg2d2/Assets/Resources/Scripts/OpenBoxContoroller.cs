﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBoxContoroller : MonoBehaviour {

	public int item_id = 1;
	private Sprite[] sp;
    public bool isOpen = false;

    void Start(){
		sp = Resources.LoadAll<Sprite>("Sprites/juelBox");
        if (isOpen)
        {
            GetComponent<SpriteRenderer>().sprite = sp[1];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = sp[0];
        }
    }

    public void OpenBox(){
        if (!isOpen)
        {
            GetComponent<SpriteRenderer>().sprite = sp[1];
            GameObject.Find("Window").transform.Find("LogWindow").gameObject.SetActive(true);
            GameObject.Find("LogWindow").GetComponent<LogController>().printText(new string[]{ItemName(item_id) + "を手に入れた！","やった！"});
			PlayerContoroller.my_items.Add (item_id);
            isOpen = true;
        }
    }

	public static string ItemName(int id){
		string item_name = ItemList.item_table.Find (x => x.item_id == id).item_name;
		return item_name;
	}
}
