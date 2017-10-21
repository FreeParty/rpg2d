using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertController : MonoBehaviour {

    public static AlertController alertController;
    public GameObject answerButton;

    // Use this for initialization
    void Start () {
        answerButton = (GameObject)Resources.Load("Prefabs/AnswerButton");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public delegate void Callback(string answer);
    
    public void Reset()
    {
        GameObject.Find("Title").GetComponent<Text>().text = null;
        GameObject.Find("Question").GetComponent<Text>().text = null;
        gameObject.SetActive(false);
    }

    public void ShowAlert(string title,string body,string[] answers,Callback callback)
    {
        gameObject.SetActive(true);
        GameObject.Find("Title").GetComponent<Text>().text = title;
        GameObject.Find("Question").GetComponent<Text>().text = body;

        Transform parent = GameObject.Find("Answers").transform;
        answerButton = (GameObject)Resources.Load("Prefabs/AnswerButton");
        foreach (Transform item in parent)
        {
            Destroy(item.gameObject);
        }

        foreach (string answerStr in answers)
        {
            GameObject answer = Instantiate(answerButton) as GameObject;
            answer.GetComponentInChildren<Text>().text = answerStr;
            answer.transform.SetParent(parent, false);
            answer.GetComponent<AnswerController>().callback = callback;
        }
    }
}
