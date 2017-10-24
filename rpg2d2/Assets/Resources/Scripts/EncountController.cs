using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EncountController : MonoBehaviour {
	GameObject player;

	void Start()
	{
        player = GameObject.Find("Player");
	}

    public void Encount()
    {
        StartCoroutine(GameObject.Find("GameManager").GetComponent<GameManager>().SceneChange("battle"));
    }


	public void RandomEncount()
	{
		int num = Random.Range (1, 1100);
		if (num == 50) 
		{
            Encount();
        }
	}
}