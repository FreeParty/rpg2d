using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour {

    public AlertController.Callback callback;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Enter()
    {
        callback(GameObject.Find("InputBody").GetComponent<Text>().text);
        AlertController.alertController.Reset();
    }
}
