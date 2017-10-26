using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolEncountContoller : MonoBehaviour {

    public int monster_num;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Encount()
    {
        GameObject.Find("BGM Source").GetComponent<BGMcontroller>().EncountSound();
        GameObject.Find("GameManager").GetComponent<GameManager>().SceneChange("battle?" + monster_num, true);
    }
}
