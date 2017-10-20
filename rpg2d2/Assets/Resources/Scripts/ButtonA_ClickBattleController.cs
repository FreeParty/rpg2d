using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonA_ClickBattleController : MonoBehaviour {

	GameObject obj;

	// Use this for initialization
	void Start () {
		obj = GameObject.Find ("management");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void onPushButton()
	{
		int current_command = CommandsController.current_command; 

		// case 1: fight, case 2: run, case 3: guard, case 4: item
		switch (current_command) 
		{
		case 1: 
			//obj.GetComponent<BattleManager> ().Fight ();
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
