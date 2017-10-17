using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogController : MonoBehaviour {
	public bool flg = false;

	void startCoroutine(){
		StartCoroutine (WaitTouch ());
	}

	public void onClick(){
		flg = true;
	}

	IEnumerator WaitTouch(){
		while(!flg) yield return null;
		flg = false;
		Debug.Log ("aaaa");
		StartCoroutine (WaitTouch ());
	}
}
