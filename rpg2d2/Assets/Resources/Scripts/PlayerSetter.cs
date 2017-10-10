using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetter : MonoBehaviour {
	public static bool setFlag = false;
	public float setX;
	public float setY;

	// Use this for initialization
	void Start () {
		if(!setFlag){
			GameObject prefab = (GameObject)Resources.Load ("Prefabs/Player");
			Vector3 pos = new Vector3(setX, setY, 0);
			GameObject player = Instantiate (prefab, pos, Quaternion.identity);
			player.name = "Player";
			setFlag = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
