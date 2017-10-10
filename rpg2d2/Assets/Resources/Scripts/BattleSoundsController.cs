using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSoundsController : MonoBehaviour {
	public AudioClip PlayerAttack;
	public AudioClip Lvup;
	public AudioClip audioClip3;
	private AudioSource audioSource;

	public void Attack(){
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = PlayerAttack;
		audioSource.Play ();
	}

	public void LvUp(){
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = Lvup;
		audioSource.Play ();
	}
}
