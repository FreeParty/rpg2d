using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour {


	public void onPush(){
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#elif UNITY_WEBPLAYER
			Application.OpenURL("http://www.yahoo.co.jp/");
		#else
			Application.Quit();
		#endif
	}
}
