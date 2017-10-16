using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBoxContoroller : MonoBehaviour {

	public int item_id = 1;
	private Sprite[] sp;
    private bool isOpen;

    void Start(){
		sp = Resources.LoadAll<Sprite>("Sprites/juelBox");
        isOpen = false;
    }

	public void OpenBox(){
        if (!isOpen)
        {
            GetComponent<SpriteRenderer>().sprite = sp[1];
            GameObject.Find("Window").transform.Find("LogWindow").gameObject.SetActive(true);
			GameObject.Find("LogWindow").GetComponent<LogController>().printText(ItemName(item_id) + "を手に入れた！");
			PlayerContoroller.my_items.Add (item_id);
            isOpen = true;
        }
    }

	public static string ItemName(int id){
		string item_name = ItemList.item_table.Find (x => x.item_id == id).item_name;
		return item_name;
	}
}
