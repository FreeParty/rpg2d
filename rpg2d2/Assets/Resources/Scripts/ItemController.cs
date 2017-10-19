using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Use()
    {
        ToggleGroup toggleGroup = GameObject.Find("ItemContainer").GetComponent<ToggleGroup>();
        print(toggleGroup.AnyTogglesOn());
        if (toggleGroup.AnyTogglesOn())
        {
            foreach(Toggle itemObj in toggleGroup.ActiveToggles())
            {
                List<int> my_items = PlayerContoroller.my_items;
                int itemNo = itemObj.GetComponent<ItemToggleController>().itemNo;
                for(int i = my_items.Count;i-- >= 0;)
                {
                    if(itemNo == my_items[i])
                    {
                        ItemList.Items item = ItemList.item_table[itemNo];
                        switch (item.item_type) //　アイテムを使う処理
                        {
                            case (int)ItemList.Eff.Hp_heal:
                                PlayerContoroller.player_status["hp"] += item.item_effect;
                                break;
                                
                        }
                        GameObject.Find("Window").transform.Find("StatusWindow").GetComponent<StatusController>().Print();
                        my_items.Remove(my_items[i]);
                        break;
                    }
                }
            }
            GameObject.Find("Description").GetComponentInChildren<Text>().text = "";
            GameObject.Find("ItemList").SetActive(false);
            GameObject.Find("Items").GetComponentInChildren<Text>().text = "どうぐ";
        }
    }

    public void Remove()
    {
        ToggleGroup toggleGroup = GameObject.Find("ItemContainer").GetComponent<ToggleGroup>();
        if (toggleGroup.AnyTogglesOn())
        {
            foreach (Toggle itemObj in toggleGroup.ActiveToggles())
            {
                List<int> my_items = PlayerContoroller.my_items;
                int itemNo = itemObj.GetComponent<ItemToggleController>().itemNo;
                for (int i = my_items.Count; i-- >= 0;)
                {
                    if (itemNo == my_items[i])
                    {
                        my_items.Remove(my_items[i]);
                        break;
                    }
                }
            }
            GameObject.Find("Description").GetComponentInChildren<Text>().text = "";
            GameObject.Find("ItemList").SetActive(false);
            GameObject.Find("Items").GetComponentInChildren<Text>().text = "どうぐ";
        }
    }
}
