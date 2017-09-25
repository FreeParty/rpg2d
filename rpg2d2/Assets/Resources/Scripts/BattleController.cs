using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class BattleController : MonoBehaviour {

	static Component[] commands;
	private GameObject[] b_commands;
	public int current_command = 1; // current command is "fight"


	// Use this for initialization
	void Start () {
		commands = gameObject.GetComponentsInChildren<Transform> (); // return ["Panel", "Fight", "run", "guard", "item"]
		commands[1].transform.Find("isSelect").GetComponent<UnityEngine.UI.Image>().enabled = true;
	}
	
	// Update is called once per frame
//	void Update () {
//		
//	}
//
//	private void FixedUpdate()
//	{
//
//	}

	public void MoveCommandCursor(float x, float y)
	{
		Debug.Log ("current_command is " + current_command);
		if (x > -0.3 && x < 0.3 && y > 0 && current_command >= 1 && current_command != 1) 
		{
			commands [current_command].transform.Find ("isSelect").GetComponent<UnityEngine.UI.Image>().enabled = false;
			current_command -= 2;
			commands [current_command].transform.Find ("isSelect").GetComponent<UnityEngine.UI.Image>().enabled = true;
		} 
		else if (x > -0.3 && x < 0.3 && y < 0 && current_command <= 7 && current_command != 7) 
		{
			commands [current_command].transform.Find ("isSelect").GetComponent<UnityEngine.UI.Image>().enabled = false;
			current_command += 2;
			commands [current_command].transform.Find ("isSelect").GetComponent<UnityEngine.UI.Image>().enabled = true;

		}

	}


}
