using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMcontroller : MonoBehaviour {

	private AudioSource audioSource;
	float startTime;

	public AudioClip encount;
	public AudioClip menu_open;
	public AudioClip menu_close;
	public AudioClip clickSound;

	// Use this for initialization
	void Start () {
		startTime = 0;
		audioSource = GetComponent<AudioSource>();
		audioSource.time = startTime;
		audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EncountSound(){
		audioSource.PlayOneShot(encount);
	}
	public void MenuOpen(){
		audioSource.PlayOneShot(menu_open);
	}
	public void MenuClose(){
		audioSource.PlayOneShot(menu_close);
	}
	public void Click(){
		audioSource.PlayOneShot(menu_close);
	}
}
