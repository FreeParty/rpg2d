using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//空のGameObjectに当てる
//Box Collider 2D も当てておく

public class PositionJumper : MonoBehaviour {
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
			GameObject player = GameObject.Find("Player");
			Vector3 pos = player.transform.position;
			pos.x = nextX;
			pos.y = nextY;
			player.transform.position = pos;
			StartCoroutine("waitPosition");
		}
	}

	IEnumerator waitPosition(){
		yield return new WaitForSeconds(1);
	}
}
