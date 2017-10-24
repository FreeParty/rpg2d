using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//空のGameObjectに当てる
//Box Collider 2D も当てておく

public class PositionJumper : MonoBehaviour {
	public float nextX;
	public float nextY;
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other) {
		if(other.CompareTag ("Player")){
            StartCoroutine(GameObject.Find("Fade").GetComponent<FadeinController>().StartFadeOut(FadeOutCallback));
        }
	}

    void FadeOutCallback()
    {
        GameObject.Find("Player").transform.position = new Vector2(nextX, nextY);
        StartCoroutine(GameObject.Find("Fade").GetComponent<FadeinController>().StartFadeIn(FadeInCallback));
    }

    void FadeInCallback()
    {
        GameObject root = GameObject.Find("GameManager").GetComponent<GameManager>().root;
        root.transform.Find("MenuModal").gameObject.SetActive(true);
    }
}
