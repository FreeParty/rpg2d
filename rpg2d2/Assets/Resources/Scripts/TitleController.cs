using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class TitleController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewGame()
    {
        GameObject.Find("Player").name = "Player_used";
        SceneManager.LoadScene("Scene/main");
    }

    public void Continue()
    {
        string json = PlayerPrefs.GetString("save");
        Dictionary<string, string> data = JsonUtility.FromJson<Serialization<string, string>>(json).ToDictionary();

        Dictionary<string, int> player_status = new Dictionary<string, int>();
        Vector3 player_position = Vector3.zero;
        string player_name = "";
        string scene_name = "";
        List<int> my_items = new List<int>();

        foreach (string key in data.Keys)
        {
            switch (key)
            {
                case "player_status":
                    player_status = JsonUtility.FromJson<Serialization<string, int>>(data[key]).ToDictionary();
                    break;
                case "player_position":
                    player_position = JsonUtility.FromJson<Vector3>(data[key]);
                    break;
                case "player_name":
                    player_name = data[key];
                    break;
                case "scene_name":
                    scene_name = data[key];
                    break;
                case "my_items":
                    my_items = JsonUtility.FromJson<Serialization<int>>(data[key]).ToList();
                    break;
            }
        }
        PlayerContoroller.player_status = player_status;
        PlayerContoroller.player_name = player_name;
        PlayerContoroller.my_items = my_items;
        GameObject.Find("Player").GetComponent<Transform>().position = player_position;
        GameObject.Find("Player").name = "Player_used";
        SceneManager.LoadScene("Scene/" + scene_name);
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
}
