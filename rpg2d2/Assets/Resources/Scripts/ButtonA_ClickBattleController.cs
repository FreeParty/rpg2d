using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonA_ClickBattleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onPushButton()
	{
		int current_command = CommandsController.current_command; 
		switch (current_command) 
		{
		case 1: 
			Debug.Log ("1");
			break;
		case 3:
			// xxxx は逃げ出した
			CommandsController.current_command = 1;
			SceneManager.LoadScene("main");
			break;
		default:
			Debug.Log ("default");
			break;

		}
	}
}
