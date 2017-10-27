using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBGMcontroller : MonoBehaviour {

	public AudioClip Normal;
	public AudioClip Boss;
	public AudioClip LastBoss;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		switch(EnemyController.enemy_status["type"]){
			default:
				audioSource.clip = Normal;
				break;
			case 0:	//雑魚
				audioSource.clip = Normal;
				break;
			case 1:	//逃げられない雑魚
				audioSource.clip = Normal;
				break;
			case 2:	//ボス
				audioSource.clip = Boss;
				break;
			case 3:	//ラスボス
				audioSource.clip = LastBoss;
				break;
		}
		audioSource.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
