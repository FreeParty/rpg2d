using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class CommandsController : MonoBehaviour {

	static Component[] commands;
	private GameObject[] b_commands;
	public static int current_command = 1; // current command is "fight"


	// Use this for initialization
	void Start () {
		commands = gameObject.GetComponentsInChildren<Transform> (); // return ["Panel", "Fight", "run", "guard", "item"]
		commands[current_command].transform.Find("isSelect").GetComponent<UnityEngine.UI.Image>().enabled = true;
	}
	

	public void MoveCommandCursor(float x, float y)
	{
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
