using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; //パネルのイメージを操作するのに必要

public class SceneJumper : MonoBehaviour {
	public string name;
	public float nextX;
	public float nextY;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other) {
		if(other.CompareTag ("Player")){
			SceneManager.LoadScene (name);
			GameObject player = GameObject.Find("Player");
			Vector3 pos = player.transform.position;
			pos.x = nextX;
			pos.y = nextY;
			player.transform.position = pos;
		}
	
	}

}
