using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolEncountContoller : MonoBehaviour {

    public int monster_num;

	// Use this for initialization
	void Start () {
		if(EnemyController.monster_num == monster_num)
        	{
        	    LogController.logController.printText(new string[] { "やったー勝ったー" });
        	}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Encount()
    {
        GameObject.Find("BGM Source").GetComponent<BGMcontroller>().EncountSound();
        GameObject.Find("GameManager").GetComponent<GameManager>().SceneChange("battle?mn=" + monster_num, true);
    }
}
