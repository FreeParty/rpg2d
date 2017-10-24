using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; //パネルのイメージを操作するのに必要

public class SceneJumper : MonoBehaviour {
	public string name;
	public float nextX;
	public float nextY;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Jump()
    {
       GameObject.Find("Player").transform.position = new Vector2(nextX, nextY);
       GameObject.Find("GameManager").GetComponent<GameManager>().SceneChange(name, false);
    }

	void OnTriggerEnter2D (Collider2D other) {
		if(other.CompareTag("Player"))
        {
            StartCoroutine(GameObject.Find("Fade").GetComponent<FadeinController>().StartFadeOut(Jump));
        }
	}
}
