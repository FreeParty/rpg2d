using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{

    Dictionary<string, bool> strongBoxes;
    bool isStateShow = false;
    public string mainSceneName;
    public GameObject root;

    // Use this for initialization
    void Start()
    {
        if (GameObject.Find("GameManager") != gameObject)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            if(SceneManager.GetActiveScene().name != "battle" && SceneManager.GetActiveScene().name != "title")
            {
                root = GameObject.Find("Window");
                LogController.logController = root.transform.Find("LogModal").gameObject.GetComponent<LogController>();
                AlertController.alertController = root.transform.Find("AlertModal").gameObject.GetComponent<AlertController>();
                mainSceneName = SceneManager.GetActiveScene().name;
            }
            else if(SceneManager.GetActiveScene().name == "battle")
            {
                root = GameObject.Find("BattleField");
                LogController.logController = root.transform.Find("LogModal").gameObject.GetComponent<LogController>();
                AlertController.alertController = root.transform.Find("AlertModal").gameObject.GetComponent<AlertController>();
                mainSceneName = "title";
            }
            else if (SceneManager.GetActiveScene().name == "title")
            {
                root = GameObject.Find("Title");
                AlertController.alertController = root.transform.Find("AlertModal").gameObject.GetComponent<AlertController>();
            }
        }
        strongBoxes = new Dictionary<string, bool>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("Scene/" + sceneName);
    }

    public void SceneChange(string sceneName,bool isFade)
    {
        OnSceneUnloaded(SceneManager.GetActiveScene());
        if (isFade)
        {
            StartCoroutine(GameObject.Find("Fade").GetComponent<FadeinController>().StartFadeOut(LoadScene, sceneName));
        }
        else
        {
            LoadScene(sceneName);
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "battle" && scene.name != "title")
        {
            root = GameObject.Find("Window");
            LogController.logController = root.transform.Find("LogModal").gameObject.GetComponent<LogController>();
            AlertController.alertController = root.transform.Find("AlertModal").gameObject.GetComponent<AlertController>();
            GameObject.Find("Player").GetComponent<Animator>().enabled = true;
            if (isStateShow)
            {
                GameObject.Find("Window").transform.Find("StatusWindow").gameObject.SetActive(true);
                GameObject.Find("MenuWindow").transform.Find("MenuButtons").transform.Find("Status").GetComponentInChildren<Text>().text = "ステータスを非表示";
            }
            foreach (GameObject strongBox in GameObject.FindGameObjectsWithTag("StrongBox"))
            {
                if (strongBoxes.ContainsKey(strongBox.name))
                {
                    strongBox.GetComponent<OpenBoxContoroller>().isOpen = strongBoxes[strongBox.name];
                }
            }
        }
        else if(scene.name == "battle")
        {
            root = GameObject.Find("BattleField");
            LogController.logController = root.transform.Find("LogModal").gameObject.GetComponent<LogController>();
            AlertController.alertController = root.transform.Find("AlertModal").gameObject.GetComponent<AlertController>();
        }
        else if (scene.name == "title")
        {
            root = GameObject.Find("Title");
            AlertController.alertController = root.transform.Find("AlertModal").gameObject.GetComponent<AlertController>();
            //プレイヤーの初期化
        }
    }

    public void OnSceneUnloaded(Scene scene)
    {
        if (scene.name != "battle" && scene.name != "title")
        {
            isStateShow = GameObject.Find("StatusWindow") != null;
            foreach (GameObject strongBox in GameObject.FindGameObjectsWithTag("StrongBox"))
            {
                if (strongBoxes.ContainsKey(strongBox.name))
                {
                    strongBoxes[strongBox.name] = strongBox.GetComponent<OpenBoxContoroller>().isOpen;
                }
                else
                {
                    strongBoxes.Add(strongBox.name, strongBox.GetComponent<OpenBoxContoroller>().isOpen);
                }
            }
            mainSceneName = scene.name;
        }
    }
    
    public void Save()
    {
        OnSceneUnloaded(SceneManager.GetActiveScene());

        string player_status = JsonUtility.ToJson(new Serialization<string, int>(PlayerContoroller.player_status), true);
        string player_position = JsonUtility.ToJson(GameObject.Find("Player").GetComponent<Transform>().position, true);
        string my_items = JsonUtility.ToJson(new Serialization<int>(PlayerContoroller.my_items), true);
        string player_name = PlayerContoroller.player_name;
        string scene_name = SceneManager.GetActiveScene().name;
        string statusWindow = isStateShow.ToString();
        string strongBoxStatus = JsonUtility.ToJson(new Serialization<string, bool>(strongBoxes), true);
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("player_status", player_status);
        data.Add("player_position", player_position);
        data.Add("player_name", player_name);
        data.Add("scene_name", scene_name);
        data.Add("my_items", my_items);
        data.Add("strongBoxes", strongBoxStatus);
        data.Add("isStateShow", statusWindow);

        string json = JsonUtility.ToJson(new Serialization<string, string>(data), true);
        PlayerPrefs.SetString("save", json);
    }

    public void Load()
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
                case "strongBoxes":
                    strongBoxes = JsonUtility.FromJson<Serialization<string, bool>>(data[key]).ToDictionary();
                    break;
                case "isStateShow":
                    if (data[key] == "True")
                    {
                        isStateShow = true;
                    }
                    else
                    {
                        isStateShow = false;
                    }
                    break;
            }
        }
        PlayerContoroller.player_status = player_status;
        PlayerContoroller.player_name = player_name;
        PlayerContoroller.my_items = my_items;
        GameObject.Find("Player").GetComponent<Transform>().position = player_position;
        SceneChange(scene_name,true);
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