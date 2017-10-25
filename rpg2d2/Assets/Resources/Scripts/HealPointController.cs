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
		LogController.logController.printText(new string[]{"HPとMPが全回復した！"});
	}
}
