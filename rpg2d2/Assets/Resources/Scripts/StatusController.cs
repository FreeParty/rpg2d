﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatusController : MonoBehaviour {
	// Use this for initialization
	void Start () {
        Print();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Print()
    {
        if (gameObject.activeSelf) {
            GameObject.Find("Name").GetComponent<Text>().text = PlayerContoroller.player_name;
            GameObject.Find("Lv").GetComponent<Text>().text = "LV : " + PlayerContoroller.player_status["lv"];
            GameObject.Find("Hp").GetComponent<Text>().text = "HP : " + Mathf.Max(PlayerContoroller.player_status["hp"],0) + " / " + PlayerContoroller.player_status["mhp"];
            GameObject.Find("Mp").GetComponent<Text>().text = "MP : " + Mathf.Max(PlayerContoroller.player_status["mp"],0) + " / " + PlayerContoroller.player_status["mmp"];
        }
    }
}
