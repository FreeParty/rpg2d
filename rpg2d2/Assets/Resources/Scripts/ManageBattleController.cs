using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageBattleController : MonoBehaviour {


	void Awake()
	{
		SceneManager.sceneUnloaded      += OnSceneUnloaded;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnSceneUnloaded( Scene scene )
	{
		int current_command = CommandsController.current_command; 
		Debug.Log ( scene.name + " scene unloaded");
	}
}
