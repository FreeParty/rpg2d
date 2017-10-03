using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

	public Text mytext;
	public string m_name="anonymas";
	public int m_hp = 1;
	public int m_mp = 1;
	public int m_attack = 1;
	public int m_guard = 1;
	public int m_ag = 1;


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
			string[,] monster_list = EnemiesData.mainSceneMonsters;
			int monster_num = selectRandomMonster (monster_list);
			mytext.text = monster_list[monster_num, 1] + " があらわれた！！"; // 名前をlogにセット
			setEnemyStatus (monster_list, monster_num);
//			GetComponent<Image>().s

			break;
		default:
			Debug.Log ("default");
			break;
		}
	}

	private int selectRandomMonster(string[,] ml)
	{
		return UnityEngine.Random.Range(0, ml.GetLength(0));
	}

//	{ "0", "スライム", "5", "0", "4", "2", "3", "0", "0" },

	private void setEnemyStatus(string [,] ml, int mn){
		m_name = ml [mn, 1];
		m_hp = int.Parse(ml [mn, 2]);
		m_mp = int.Parse(ml [mn, 3]);
		m_attack = int.Parse(ml [mn, 4]);
		m_guard = int.Parse(ml [mn, 5]);
		m_ag = int.Parse(ml [mn, 6]);
	}

}
