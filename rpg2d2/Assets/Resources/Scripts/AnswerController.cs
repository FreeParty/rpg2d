using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerController : MonoBehaviour {

    public AlertController.Callback callback;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Select()
    {
        AlertController.alertController.Reset();
        callback(GetComponentInChildren<Text>().text);
    }
}
