using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  PlayerContoroller : MonoBehaviour {
	public int HP = 30;
	public int Max_HP = 30;
	public int MP = 13;
	public int Max_MP = 13;
	public int Attack = 12;
	public int Guard = 12;
	public int Agility = 12;

	public string objectNameTag;
	public bool StrongBoxFlg;


	void Start(){
		objectNameTag = "HAA";
	}

	public void CheckObject(){ // call if push A button
		Debug.Log ("tag2 is " + objectNameTag);
		if(objectNameTag == "StrongBox"){
			StrongBoxFlg = true;
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		objectNameTag = coll.gameObject.tag;
	}

	void OnCollisionExit2D(Collision2D coll){
		objectNameTag = "";
	}


	void OnCollisionStay2D(Collision2D coll) {

		if (coll.gameObject.tag == "StrongBox" && StrongBoxFlg) {
			OpenBoxContoroller op = coll.gameObject.GetComponent<OpenBoxContoroller> ();
			op.OnOpenedBox ();
			StrongBoxFlg = false;
		}
	}



}