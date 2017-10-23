using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleCommands : MonoBehaviour
{

    GameObject mng;
    public GameObject ItemButton;

    // Use this for initialization
    void Start()
    {
        mng = GameObject.Find("Management");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fight()
    {
        mng.GetComponent<BattleManager>().Fight();
    }

    public void Run()
    {
        BattleManager.ToggleCommands();
        LogController.logController.printText(new string[] { PlayerContoroller.player_name + "は逃げだした。" }).then(new LogController.Callback(BattleManager.BackField));
    }

    public void Guard()
    {

    }

    public void Item()
    {
        BattleManager.ToggleCommands();
        if (PlayerContoroller.my_items.Count > 0)
        {
            GameObject.Find("BattleField").transform.Find("ItemListInBattle").gameObject.SetActive(true);
            Transform parent = GameObject.Find("ItemContainer").transform;
            foreach (Transform item in parent)
            {
                Destroy(item.gameObject);
            }

            ItemButton = (GameObject)Resources.Load("Prefabs/ItemButton");
            PlayerContoroller.my_items.Sort();
            foreach (int itemNo in PlayerContoroller.my_items)
            {
                GameObject item = Instantiate(ItemButton) as GameObject;
                item.GetComponentInChildren<ItemToggleController>().itemNo = itemNo;
                item.GetComponentInChildren<Toggle>().group = GameObject.Find("ItemContainer").GetComponent<ToggleGroup>();
                item.GetComponentInChildren<Text>().text = ItemList.item_table[itemNo].item_name;
                item.transform.SetParent(parent, false);
            }
        }
        else
        {
            GameObject.Find("BattleField").transform.Find("LogWindow").gameObject.SetActive(true);
            GameObject.Find("LogWindow").GetComponent<LogController>().printText(new string[] { "どうぐを持っていません。" }).then(new LogController.Callback(BattleManager.ToggleCommands));
        }
    }
}
