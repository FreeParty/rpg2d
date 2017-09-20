using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncountController : MonoBehaviour {

	GameObject fadeObj; // fade in/out を制御するオブジェクト
	FadeinController m_fade;

	void Start()
	{
		fadeObj = GameObject.Find ("fade/Panel");
		m_fade = fadeObj.GetComponent<FadeinController> ();
	}

	public void RandomEncount()
	{
		int num = UnityEngine.Random.Range (0, 101);
		if (num == 50) 
		{
			m_fade.alfa = 0;
			m_fade.isFadeOut = true;
		}
	}

}
