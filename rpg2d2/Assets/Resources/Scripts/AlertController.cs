using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertController : MonoBehaviour {

    public static AlertController alertController;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public delegate void Callback(string answer);
    
    public void Reset()
    {
        GameObject.Find("AlertTitle").GetComponent<Text>().text = null;
        GameObject.Find("Question").GetComponent<Text>().text = null;
        gameObject.SetActive(false);
    }

    public void ShowAlertByOptions(string title,string body,string[] options,Callback callback)
    {
        gameObject.SetActive(true);
        GameObject.Find("AlertTitle").GetComponent<Text>().text = title;
        GameObject.Find("Question").GetComponent<Text>().text = body;

        Transform parent = GameObject.Find("Options").transform;
        GameObject optionButton = (GameObject)Resources.Load("Prefabs/OptionButton");
        foreach (Transform item in parent)
        {
            Destroy(item.gameObject);
        }

        foreach (string option in options)
        {
            GameObject answer = Instantiate(optionButton) as GameObject;
            answer.GetComponentInChildren<Text>().text = option;
            answer.transform.SetParent(parent, false);
            answer.GetComponent<AnswerController>().callback = callback;
        }
    }
    public void ShowAlertByInput(string title, string body,string defaultInput ,Callback callback)
    {
        gameObject.SetActive(true);
        GameObject.Find("AlertTitle").GetComponent<Text>().text = title;
        GameObject.Find("Question").GetComponent<Text>().text = body;

        Transform parent = GameObject.Find("Options").transform;
        foreach (Transform item in parent)
        {
            Destroy(item.gameObject);
        }
        GameObject inputField = (GameObject)Resources.Load("Prefabs/InputField");
        GameObject input = Instantiate(inputField) as GameObject;
        input.transform.SetParent(parent, false);
        GameObject.Find("Placeholder").GetComponent<Text>().text = defaultInput;
        GameObject.Find("Enter").GetComponent<InputController>().callback = callback;
        GameObject.Find("Enter").GetComponent<InputController>().defaultInput = defaultInput;
    }
}
