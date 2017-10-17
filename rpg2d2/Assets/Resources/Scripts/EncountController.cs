using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncountController : MonoBehaviour {
	GameObject player;

	void Start()
	{
        player = GameObject.Find("Player");
	}

    public void Encount()
    {
        GameObject.Find("Controller").SetActive(false);// 仮想コントローラー
        GameObject.Find("StatusWindow").SetActive(false);
        SceneManager2d.isEncount = true;
        FadeinController m_fade = GameObject.Find("Window").GetComponent<FadeinController>();
        m_fade.alfa = 0;
        m_fade.isFadeOut = true;
        player.GetComponent<Animator>().enabled = false;
        player.name = "Player_used";
    }


	public void RandomEncount()
	{
		int num = Random.Range (1, 100);
		if (num == 50) 
		{
            Encount();
        }
	}

	public void InsertBattleScene()
	{
		SceneManager2d.current_scene = SceneManager.GetActiveScene ().name;
		SceneManager.LoadScene("Scene/battle");
	}
		
}