using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBoxContoroller : MonoBehaviour {

	public string treasure_name = "item00";
	private Sprite[] sp;

	void Start(){
		sp = Resources.LoadAll<Sprite>("Sprites/juelBox");
	}

//	void OnCollisionStay2D(Collision2D coll) {
//		
//		if (coll.gameObject.name == "Player" && Input.GetKeyDown("space")) {
//			GetComponent<SpriteRenderer> ().sprite = sp[1];
//			Debug.Log ("Hooo");
//		}
//	}

	public void OnOpenedBox(){
		GetComponent<SpriteRenderer> ().sprite = sp[1];
	}
}
