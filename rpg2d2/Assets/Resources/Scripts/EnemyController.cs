using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

	public Text mytext;
	private Sprite sp;
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


	/*
	 * Resources/enemys/シーン名 のディレクトリにモンスターの画像を格納してください
	 * また、シーンを複数作っていて、そのシーンによって出現するモンスターを切り替えたい場合、シーン名と上のシーン名を統一してください
	 * setEnemyStatus() でエンカウントしたシーンと、そのシーン名に同じResources/enemy/~ からモンスターの画像を引っ張ってきます
	*/
	public void SetEnemyData(){
		string reccurent_scene = SceneManager2d.current_scene;

		switch (reccurent_scene) {
		case "main":
			string[,] monster_list = EnemiesData.mainSceneMonsters;
			int monster_num = selectRandomMonster (monster_list);
			setEnemyStatus (monster_list, monster_num, reccurent_scene);
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

	private void setEnemyStatus(string [,] ml, int mn, string rc){
		mytext.text = ml[mn, 1] + " があらわれた！！"; // 名前をlogにセット
		sp = GetSprite ("enemys/" + rc, ml[mn, 0]);
		GetComponent<Image> ().sprite = sp;
		m_name = ml [mn, 1];
		m_hp = int.Parse(ml [mn, 2]);
		m_mp = int.Parse(ml [mn, 3]);
		m_attack = int.Parse(ml [mn, 4]);
		m_guard = int.Parse(ml [mn, 5]);
		m_ag = int.Parse(ml [mn, 6]);
	}


	// @param fileName ファイル名
	// @param spriteName スプライト名
	public static Sprite GetSprite(string fileName, string spriteName) {
		Sprite[] sprites = Resources.LoadAll<Sprite>(fileName);
		return System.Array.Find<Sprite>(sprites, (sprite) => sprite.name.Equals(spriteName));
	}

}
