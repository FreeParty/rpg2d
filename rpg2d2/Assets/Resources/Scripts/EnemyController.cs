using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

	public static Dictionary<string, int> enemy_status = new Dictionary<string, int> () {
		{"hp", 1},
		{"mhp", 1},
		{"mp", 1},
		{"mmp", 1},
		{"at", 1},
		{"df", 1},
		{"ag", 1},
		{"get_exp", 0},
		{"get_money", 5},
		{"type", 0},
		{"drop", 0},
		{"drop_pro", 0},
	};

	public static string monster_name = "0";
    
	private Sprite sp;

	// Use this for initialization
	void Awake() {
		SetEnemyData ();

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
			break;
		}
	}

	private int selectRandomMonster(string[,] ml)
	{
		return UnityEngine.Random.Range(0, ml.GetLength(0));
	}

//[0]NO, [1] name, [2] HP, [3]MP, [4]attack, [5]guarg, [6]ag, [7]type, [8] drop_no, [9] get_exp, [10] get_money, 

	private void setEnemyStatus(string [,] ml, int mn, string rc){
		monster_name = ml[mn, 1];
        GameObject.Find("LogWindow").GetComponent<LogController>().printText(new string[] { monster_name + " があらわれた！！\n" }).then(new LogController.Callback(BattleManager.ToggleCommands)); // 名前をlogにセット
		sp = GetSprite ("enemys/" + rc, ml[mn, 0]);
		GetComponent<Image> ().sprite = sp;
		enemy_status["hp"] = int.Parse(ml [mn, 2]);
		enemy_status["mhp"] = int.Parse(ml [mn, 2]);
		enemy_status["mp"] = int.Parse(ml [mn, 3]);
		enemy_status["mmp"] = int.Parse(ml [mn, 3]);
		enemy_status["at"] = int.Parse(ml [mn, 4]);
		enemy_status["df"] = int.Parse(ml [mn, 5]);
		enemy_status["ag"] = int.Parse(ml [mn, 6]);
		enemy_status ["type"] = int.Parse (ml [mn, 7]);
		enemy_status ["drop"] = int.Parse (ml [mn, 8]);
		enemy_status ["get_exp"] = int.Parse (ml [mn, 9]);
		enemy_status ["get_money"] = int.Parse (ml [mn, 10]);
		enemy_status ["drop_pro"] = int.Parse (ml [mn, 11]);
	}


	// @param fileName ファイル名
	// @param spriteName スプライト名
	public static Sprite GetSprite(string fileName, string spriteName) {
		Sprite[] sprites = Resources.LoadAll<Sprite>(fileName);
		return System.Array.Find<Sprite>(sprites, (sprite) => sprite.name.Equals(spriteName));
	}

}
