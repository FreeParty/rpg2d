using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMcontroller : MonoBehaviour {

	private AudioSource audioSource;
	float startTime;

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
}
