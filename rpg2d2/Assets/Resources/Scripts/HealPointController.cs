using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPointController : MonoBehaviour {

	AudioSource audio;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Healing(){
		audio = GetComponent<AudioSource>();
		audio.PlayOneShot(audio.clip);
		PlayerContoroller.player_status["hp"] = PlayerContoroller.player_status["mhp"];
		PlayerContoroller.player_status["mp"] = PlayerContoroller.player_status["mmp"];
		GameObject statusWindow = GameObject.Find("Window").transform.Find("StatusWindow").gameObject;
		statusWindow.GetComponent<StatusController>().Print();
		LogController.logController.printText(new string[]{"HPとMPが全回復した！"});
	}
}
