using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonA_ClickContoroller : MonoBehaviour {

	GameObject player;

	void Start(){
		player = GameObject.Find ("Player");
	}
	
	public void onPushCheckObject(){ // call if push A button
        PlayerContoroller pc = player.GetComponent<PlayerContoroller>();
        pc.CheckObject();
    }
}
