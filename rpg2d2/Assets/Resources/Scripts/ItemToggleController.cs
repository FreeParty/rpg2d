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
        ItemList.Items item = ItemList.item_table[itemNo];
        Texture2D texture = Resources.Load(item.item_img) as Texture2D;
        GameObject.Find("ItemImage").GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        GameObject.Find("Description").GetComponentInChildren<Text>().text = "【" + item.item_name + "】\n" + item.item_desc;
    }
}
