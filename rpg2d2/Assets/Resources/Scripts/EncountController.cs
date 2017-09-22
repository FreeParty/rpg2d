using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncountController : MonoBehaviour {

	GameObject fadeObj; // fade in/out を制御するオブジェクト
	FadeinController m_fade;
	GameObject stickUI;
	Canvas canvas;


	void Start()
	{
		fadeObj = GameObject.Find ("fade/Panel");
		m_fade = fadeObj.GetComponent<FadeinController> ();
		canvas = GameObject.Find("MobileSingleStickControl").GetComponent<Canvas>();
	}

	public void RandomEncount()
	{
		int num = UnityEngine.Random.Range (0, 101);
		if (num == 50) 
		{
			canvas.enabled = false;
			m_fade.alfa = 0;
			m_fade.isFadeOut = true;
			Time.timeScale = 0;
		}
	}

}
