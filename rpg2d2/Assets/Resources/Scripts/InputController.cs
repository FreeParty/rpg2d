using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour {

    public AlertController.Callback callback;
    public string defaultInput;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Enter()
    {
        string input = GameObject.Find("InputBody").GetComponent<Text>().text;
        if (input.Length > 0)
        {
            callback(GameObject.Find("InputBody").GetComponent<Text>().text);
            AlertController.alertController.Reset();
        }
        else
        {
            callback(defaultInput);
        }
        AlertController.alertController.Reset();
    }
}
