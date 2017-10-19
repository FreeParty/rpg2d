using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

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

    public static void CloseMenu()
    {

        if (GameObject.Find("MenuButtons") != null)
        {
            GameObject.Find("MenuWindow").GetComponent<RectTransform>().sizeDelta = new Vector2(200, 50);
            GameObject.Find("MenuButtons").SetActive(false);
            GameObject.Find("Menu").GetComponentInChildren<Text>().text = "メニュー";
            if (GameObject.Find("ItemList") != null)
            {
                GameObject.Find("ItemList").SetActive(false);
            }
        }
    }

    public void Menu()
    {
        if(GameObject.Find("MenuButtons") != null)
        {
            CloseMenu();
            GameObject.Find("Window").GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        }
        else
        {
            GameObject.Find("Window").GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
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
        string player_status = JsonUtility.ToJson(new Serialization<string,int>(PlayerContoroller.player_status),true);
        string player_position = JsonUtility.ToJson(GameObject.Find("Player").GetComponent<Transform>().position, true);
        string my_items = JsonUtility.ToJson(new Serialization<int>(PlayerContoroller.my_items), true);
        string player_name = PlayerContoroller.player_name;
        string scene_name = SceneManager.GetActiveScene().name;
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("player_status", player_status);
        data.Add("player_position", player_position);
        data.Add("player_name", player_name);
        data.Add("scene_name", scene_name);
        data.Add("my_items", my_items);

        string json = JsonUtility.ToJson(new Serialization<string, string>(data),true);
        PlayerPrefs.SetString("save", json);
        GameObject.Find("MenuWindow").GetComponent<RectTransform>().sizeDelta = new Vector2(200, 50);
        GameObject.Find("MenuButtons").SetActive(false);
        GameObject.Find("Menu").GetComponentInChildren<Text>().text = "メニュー";
        if (GameObject.Find("ItemList") != null)
        {
            GameObject.Find("ItemList").SetActive(false);
        }
        GameObject.Find("Window").transform.Find("LogWindow").gameObject.SetActive(true);
        GameObject.Find("LogWindow").GetComponent<LogController>().printText(new string[] { "セーブしました。" });
    }

    // List<T>
    [Serializable]
    public class Serialization<T>
    {
        [SerializeField]
        List<T> target;
        public List<T> ToList() { return target; }

        public Serialization(List<T> target)
        {
            this.target = target;
        }
    }

    // Dictionary<TKey, TValue>
    [Serializable]
    public class Serialization<TKey, TValue> : ISerializationCallbackReceiver
    {
        [SerializeField]
        List<TKey> keys;
        [SerializeField]
        List<TValue> values;

        Dictionary<TKey, TValue> target;
        public Dictionary<TKey, TValue> ToDictionary() { return target; }

        public Serialization(Dictionary<TKey, TValue> target)
        {
            this.target = target;
        }

        public void OnBeforeSerialize()
        {
            keys = new List<TKey>(target.Keys);
            values = new List<TValue>(target.Values);
        }

        public void OnAfterDeserialize()
        {
            var count = Math.Min(keys.Count, values.Count);
            target = new Dictionary<TKey, TValue>(count);
            for (var i = 0; i < count; ++i)
            {
                target.Add(keys[i], values[i]);
            }
        }
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