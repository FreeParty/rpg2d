using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void Menu()
    {
        if(GameObject.Find("MenuButtons") != null)
        {
            GameObject.Find("MenuWindow").GetComponent<RectTransform>().sizeDelta = new Vector2(200, 50);
            GameObject.Find("MenuButtons").SetActive(false);
            GameObject.Find("Menu").GetComponentInChildren<Text>().text = "メニュー";
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
            GameObject.Find("ItemList").SetActive(false);
            GameObject.Find("Items").GetComponentInChildren<Text>().text = "どうぐ";
        }
        else
        {
            GameObject.Find("Window").transform.Find("ItemList").gameObject.SetActive(true);
            Transform parent = GameObject.Find("ItemContainer").transform;
            foreach (Transform item in parent)
            {
                GameObject.Destroy(item.gameObject);
            }

            ItemButton = (GameObject)Resources.Load("Prefabs/ItemButton");
            foreach (int itemNo in PlayerContoroller.my_items)
            {
                GameObject item = Instantiate(ItemButton) as GameObject;
                item.GetComponentInChildren<Text>().text = ItemList.item_table[itemNo].item_name;
                item.transform.SetParent(parent,false);
            }
            GameObject.Find("Items").GetComponentInChildren<Text>().text = "どうぐを閉じる";
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

    }

    public void End()
    {
        #if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
			Application.OpenURL("http://www.yahoo.co.jp/");
        #else
            Application.Quit();
        #endif
    }
}