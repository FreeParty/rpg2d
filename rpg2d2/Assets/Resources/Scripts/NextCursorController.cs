using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextCursorController : MonoBehaviour {

	private Text text;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		StartCoroutine (Flash (0.7f));
	}
	
	IEnumerator Flash(float num){
		while (true) {
			yield return new WaitForSeconds (num);
			text.enabled = !text.enabled;
		}
	}
}
