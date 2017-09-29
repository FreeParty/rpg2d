using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SetEnemyData ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetEnemyData(){
		string field_name = SceneManager2d.current_scene;

		switch (field_name) {
		case "main":
			Debug.Log ("Pass");
			string[,] monster_list = EnemiesData.mainSceneMonsters;
//			string[] mons = GetRandom (monster_list);
			Debug.Log ("mons " + monster_list[UnityEngine.Random.Range(0, monster_list.GetLength(0)),1]);
			break;
		default:
			Debug.Log ("default");
			break;
		}
	}


}
