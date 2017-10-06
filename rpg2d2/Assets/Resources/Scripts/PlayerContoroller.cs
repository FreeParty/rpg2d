using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  PlayerContoroller : MonoBehaviour {

	public static Dictionary<string, int> player_status = new Dictionary<string, int> () {
		{"hp", 25},
		{"mhp", 25},
		{"mp", 0},
		{"mmp", 0},
		{"at", 2},
		{"df", 1},
		{"ag", 4},
		{"lv", 1},
		{"exp", 0},
		{"money", 0}
	};

	public static int[] have_items = {};
	public static string player_name = "sample";

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