using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSoundsController : MonoBehaviour {
	public AudioClip PlayerAttack;
	public AudioClip CriticalHit;
	public AudioClip MissSound;
	public AudioClip GuardSound;
	public AudioClip DeadSound;
	public AudioClip RunSound;
	public AudioClip Lvup;
	public AudioClip DropSound;
	
	private AudioSource audioSource;

	public void Attack(){
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = PlayerAttack;
		audioSource.Play ();
	}

	public void Critical(){
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = CriticalHit;
		audioSource.Play ();
	}

	public void Miss(){
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = MissSound;
		audioSource.Play ();
	}

	public void Guard(){
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = GuardSound;
		audioSource.Play ();
	}

	public void Dead(){
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = DeadSound;
		audioSource.Play ();
	}

	public void Run(){
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = RunSound;
		audioSource.Play ();
	}

	public void LvUp(){
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = Lvup;
		audioSource.Play ();
	}

	public void Drop(){
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = DropSound;
		audioSource.Play ();
	}
}
