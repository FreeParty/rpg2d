using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonA_ClickContoroller : MonoBehaviour {

	GameObject player;

	void Start(){
		player = GameObject.Find ("Player");
	}
	
	public void onPushCheckObject(){ // call if push A button

		PlayerContoroller pc = player.GetComponent<PlayerContoroller> ();
		Debug.Log ("tag2 is " + pc.objectNameTag);
		if(pc.objectNameTag == "StrongBox"){
			Debug.Log ("hoge1");
			pc.StrongBoxFlg = true;
		}
	}
}
