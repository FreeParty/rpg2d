using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  PlayerContoroller : MonoBehaviour {
	public static int HP = 30;
	public static int Max_HP = 30;
	public static int MP = 13;
	public static int Max_MP = 13;
	public static int Attack = 12;
	public static int Guard = 12;
	public static int Agility = 12;

	public string objectNameTag;
	public bool StrongBoxFlg;

	GameObject player_used;


	void Awake(){
		DontDestroyOnLoad(this);
		player_used =  GameObject.Find ("Player_used");
		if (player_used) {
			player_used.GetComponent<Animator> ().enabled = true;
			Destroy (GameObject.Find ("Player"));
			player_used.name = "Player";
		}
	}
	void Start(){
		objectNameTag = "HAA";
	}

	public void CheckObject(){ // Calling if push A button
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