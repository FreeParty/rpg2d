using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject ItemButton;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private static void CloseItemList()
    {
        if (GameObject.Find("ItemList") != null)
        {
            GameObject.Find("Description").GetComponentInChildren<Text>().text = "";
            GameObject.Find("ItemImage").GetComponent<Image>().sprite = null;
            GameObject.Find("ItemImage").GetComponent<Image>().color = new Color(255, 255, 255, 0);
            GameObject.Find("ItemList").SetActive(false);
            GameObject.Find("Items").GetComponentInChildren<Text>().text = "どうぐ";
        }
    }

    public static void CloseMenu()
    {
        if (GameObject.Find("MenuButtons") != null)
        {
            CloseItemList();
            GameObject.Find("MenuWindow").GetComponent<RectTransform>().sizeDelta = new Vector2(200, 50);
            GameObject.Find("MenuButtons").SetActive(false);
            GameObject.Find("Menu").GetComponentInChildren<Text>().text = "メニュー";
        }
    }

    public void Menu()
    {
        if (GameObject.Find("MenuButtons") != null)
        {
            CloseMenu();
        }
        else
        {
            GameObject.Find("MenuWindow").GetComponent<RectTransform>().sizeDelta = new Vector2(200, 220);
            GameObject.Find("MenuWindow").transform.Find("MenuButtons").gameObject.SetActive(true);
            GameObject.Find("Menu").GetComponentInChildren<Text>().text = "閉じる";
        }
    }

    public void Items()
    {
        if (GameObject.Find("ItemList") != null)
        {
            CloseItemList();
        }
        else
        {
            if (PlayerContoroller.my_items.Count > 0)
            {
                GameObject.Find("Window").transform.Find("ItemList").gameObject.SetActive(true);
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
                GameObject.Find("Items").GetComponentInChildren<Text>().text = "どうぐを閉じる";
            }
            else
            {
                GameObject.Find("Window").transform.Find("LogWindow").gameObject.SetActive(true);
                GameObject.Find("LogWindow").GetComponent<LogController>().printText(new string[] { "どうぐを持っていません。" });
                CloseMenu();
            }
        }
    }

    public void Status()
    {
        if (GameObject.Find("StatusWindow") != null)
        {
            GameObject.Find("StatusWindow").SetActive(false);
            GameObject.Find("Status").GetComponentInChildren<Text>().text = "ステータス";
        }
        else
        {
            GameObject.Find("Window").transform.Find("StatusWindow").gameObject.SetActive(true);
            GameObject.Find("Status").GetComponentInChildren<Text>().text = "ステータスを非表示";
        }
    }

    public void Save()
    {
        CloseItemList();
        GameObject.Find("MenuWindow").GetComponent<RectTransform>().sizeDelta = new Vector2(200, 50);
        GameObject.Find("MenuButtons").SetActive(false);
        GameObject.Find("Menu").GetComponentInChildren<Text>().text = "メニュー";
        GameObject.Find("GameManager").GetComponent<GameManager>().Save();
        GameObject.Find("Window").transform.Find("LogWindow").gameObject.SetActive(true);
        GameObject.Find("LogWindow").GetComponent<LogController>().printText(new string[] { "セーブしました。" });
    }

    public void End()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().SceneChange("title");
    }
}