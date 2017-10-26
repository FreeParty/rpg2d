using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemController : MonoBehaviour
{

    GameObject statusWindow;
    GameObject root;
	  BattleManager bm;

    // Use this for initialization
    void Start()
    {
		if (SceneManager.GetActiveScene ().name == "battle") 
		{
			root = GameObject.Find ("BattleField");
			statusWindow = GameObject.Find ("StatusWindowInBattle");
			bm = GameObject.Find ("Management").GetComponent<BattleManager> ();
		}
        else
        {
            statusWindow = GameObject.Find("Window").transform.Find("StatusWindow").gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Use()
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
                        ItemList.Items item = ItemList.item_table[itemNo];
						if (SceneManager.GetActiveScene ().name == "battle")  //戦闘シーんのとき
						{
							bm.UseItem(item);
						}
						else // 戦闘フィールド以外でアイテムを使った処理
						{
							switch (item.item_type) // アイテムを使う処理
							{
								case (int)ItemList.Eff.Hp_heal:
									PlayerContoroller.player_status["hp"] += item.item_effect;
									break;
							}
							LogController.logController.printText(new string[] { item.item_name + "を使った" });
						}
                        my_items.Remove(my_items[i]);
                        break;
                    }
                }
            }
			Back ();
            statusWindow.GetComponent<StatusController>().Print();
        }
    }

    public void Back()
    {
        GameObject.Find("Description").GetComponentInChildren<Text>().text = "";
        GameObject.Find("ItemImage").GetComponent<Image>().sprite = null;
        if (SceneManager.GetActiveScene().name == "battle")
        {
            BattleManager.ToggleCommands();
            GameObject.Find("ItemListInBattle").SetActive(false);
        }
        else
        {
            GameObject.Find("Items").GetComponentInChildren<Text>().text = "どうぐ";
            MenuController.CloseMenu();
        }
    }

    public void RemoveCallback(string option)
    {
        switch (option)
        {
            case "はい":
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
                }
                Back();
                break;
            case "いいえ":
                break;
        }
    }

    public void Remove()
    {
        AlertController.alertController.ShowAlertByOptions("捨てる", "本当に捨てますか？", new string[] { "はい", "いいえ" }, RemoveCallback);
    }

}