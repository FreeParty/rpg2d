using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Print();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Print()
    {
        Transform statusWindow = GameObject.Find("Window").transform.Find("StatusWindow");
        statusWindow.Find("name").GetComponent<Text>().text = PlayerContoroller.player_name;
        statusWindow.Find("Lv").GetComponent<Text>().text = "LV : " + PlayerContoroller.player_status["lv"];
        statusWindow.Find("Hp").GetComponent<Text>().text = "HP : " + PlayerContoroller.player_status["hp"] + " / " + PlayerContoroller.player_status["mhp"];
        statusWindow.Find("Mp").GetComponent<Text>().text = "MP : " + PlayerContoroller.player_status["mp"] + " / " + PlayerContoroller.player_status["mmp"];
    }
}
