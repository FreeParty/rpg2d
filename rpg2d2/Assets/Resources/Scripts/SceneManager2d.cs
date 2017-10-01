using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager2d : MonoBehaviour {

	public static string current_scene;
	public static bool isEncount;

	// Use this for initialization
	void Start () {
		current_scene = "main";
		isEncount = false;
	}

}
