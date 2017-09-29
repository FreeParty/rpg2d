using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncountController : MonoBehaviour {

	GameObject fadeObj; // fade in/out を制御するオブジェクト
	FadeinController m_fade;
	GameObject stickUI;
	Canvas canvas;
	GameObject player;



	void Start()
	{
		fadeObj = GameObject.Find ("fade/Panel");
		player = GameObject.Find ("Player");
		m_fade = fadeObj.GetComponent<FadeinController> ();
		canvas = GameObject.Find("GameController").GetComponent<Canvas>();
		stickUI = GameObject.Find ("GameController/MobileJoystick");
	}


	public void RandomEncount()
	{
		int num = UnityEngine.Random.Range (0, 101);
		if (num == 50) 
		{
			canvas.enabled = false; // 仮想コントローラーを非表示
			m_fade.alfa = 0;
			m_fade.isFadeOut = true;
			player.GetComponent<Animator> ().enabled = false;
			stickUI.SetActive (false);
			player.name = "Player_used";
		}
	}

	public void InsertBattleScene()
	{
		SceneManager.LoadScene("Scene/battle");
	}

}
