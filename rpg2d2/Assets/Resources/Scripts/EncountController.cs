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
        FadeinController m_fade = GameObject.Find("Window").GetComponent<FadeinController>();
        m_fade.alfa = 0;
        m_fade.isFadeOut = true;
        StartCoroutine("InsertBattleScene");
    }


	public void RandomEncount()
	{
		int num = Random.Range (1, 1100);
		if (num == 50) 
		{
            Encount();
        }
	}

    IEnumerator InsertBattleScene()
	{
        yield return new WaitUntil(() => GameObject.Find("Window").GetComponent<FadeinController>().isFadeOut == false);
        GameObject.Find("GameManager").GetComponent<GameManager>().SceneChange("battle");
	}
		
}