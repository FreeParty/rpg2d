using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour {

	public GameObject log_obj;
	public GameObject name_obj;
	public GameObject hp_obj;
	public GameObject mp_obj;
	public GameObject a_button;
	public GameObject b_button;
	public GameObject sound_box;
	public GameObject commands;
	private string kaisin;
	private string tukon;

	void Start(){

		Debug.Log ("hoge " + StatusData.LvupPlayerStatus [2,0]);
		name_obj.GetComponent<Text> ().text = PlayerContoroller.player_name;
		hp_obj.GetComponent<Text> ().text = PlayerContoroller.player_status["hp"].ToString ();
		mp_obj.GetComponent<Text> ().text = PlayerContoroller.player_status["mp"].ToString ();
	}


	// たたかう が押された時に発火
	public void Fight(){

		kaisin = "";
		tukon = "";
		int p_damage = P_damage ();
		int e_damage = E_damage ();

		int first = compare_ag ();
		if (first == 1) { // player が先手
			sound_box.GetComponent<BattleSoundsController> ().Attack ();
			EnemyController.enemy_status["hp"] -= p_damage;

			if (EnemyController.enemy_status ["hp"] <= 0) { // エネミーのhpが0になったとき
				log_obj.GetComponent<Text>().text = string.Format("{0}のこうげき\n{1}{2}に<color=#ff0000>{3}</color>のダメージ\n{2}をたおした！\n<color=#fce700>{4}</color>の経験値と<color=#fce700>{5}</color>のゴールドを手に入れた！", 
					PlayerContoroller.player_name, kaisin, EnemyController.monster_name, p_damage, EnemyController.enemy_status["get_exp"], EnemyController.enemy_status["get_money"]);
				Enemy_die ();
			} 

			PlayerContoroller.player_status ["hp"] -= e_damage;
			if (PlayerContoroller.player_status ["hp"] <= 0) { // player のhpが0になったとき
				Player_die ();
			} 

			if (PlayerContoroller.player_status ["hp"] >= 0 && EnemyController.enemy_status ["hp"] >= 0) {
				log_obj.GetComponent<Text>().text = string.Format("{0}のこうげき\n{1}{2}に<color=#ff0000>{3}</color>のダメージ\n{2}のこうげき\n{4}{0}に<color=#ff0000>{5}</color>のダメージ\n", 
					PlayerContoroller.player_name, kaisin, EnemyController.monster_name, p_damage, tukon, e_damage);
			}
			
		} else {

			sound_box.GetComponent<BattleSoundsController> ().Attack ();
			PlayerContoroller.player_status ["hp"] -= e_damage;
			if (PlayerContoroller.player_status ["hp"] <= 0) {
				Player_die ();
			}


			EnemyController.enemy_status["hp"] -= p_damage;
			if (EnemyController.enemy_status ["hp"] <= 0) {
				log_obj.GetComponent<Text>().text = string.Format("{0}のこうげき\n{1}{2}に<color=#ff0000>{3}</color>のダメージ\n{2}のこうげき\n{4}{0}に<color=#ff0000>{5}</color>のダメージ\n{0}をたおした！\n<color=#fce700>{6}</color>の経験値と<color=#fce700>{7}</color>のゴールドを手に入れた！", 
					EnemyController.monster_name, tukon, PlayerContoroller.player_name, e_damage, kaisin, p_damage, EnemyController.enemy_status["get_exp"], EnemyController.enemy_status["get_money"]);
				Enemy_die ();
			}

			if (PlayerContoroller.player_status ["hp"] >= 0 && EnemyController.enemy_status ["hp"] >= 0) {
				log_obj.GetComponent<Text>().text = string.Format("{0}のこうげき\n{1}{2}に<color=#ff0000>{3}</color>のダメージ\n{2}のこうげき\n{4}{0}に<color=#ff0000>{5}</color>のダメージ\n", 
					EnemyController.monster_name, tukon, PlayerContoroller.player_name, e_damage, kaisin, p_damage);

			}
		}

		hp_obj.GetComponent<Text> ().text = PlayerContoroller.player_status ["hp"].ToString (); // HP 更新

	}


	public int P_damage(){ // プレイヤーが与えるダメージ

		int p_damage = 0;

		// 会心の一撃
		if (Random.Range (1, 3) == 1) {
			Debug.Log ("aaaa");
			p_damage = (int)(PlayerContoroller.player_status ["at"] * Random.Range (0.6f, 1.5f)) + Random.Range (1, PlayerContoroller.player_status ["ag"]);
			kaisin = "<color=#fce700>会心の一撃！ </color>";
			return p_damage;
		}


		if (PlayerContoroller.player_status ["at"] - EnemyController.enemy_status ["df"] > 0) {
			p_damage = (int)(PlayerContoroller.player_status ["at"] - EnemyController.enemy_status ["df"] * Random.Range (0.6f, 1.5f)) + Random.Range (1, PlayerContoroller.player_status ["ag"]);
		} else {
			p_damage = (int)(Random.Range(1, 3) * Random.Range (0.6f, 1.5f));
		}

		return p_damage;
			
	}

	public int E_damage(){
		
		int e_damage = 0;

		// 痛恨の一撃
		if (Random.Range (1, 32) == 1) {
			e_damage = (int)(EnemyController.enemy_status ["at"] * Random.Range (0.6f, 1.5f)) + Random.Range (1, EnemyController.enemy_status ["ag"]);
			tukon = "<color=#fce700>痛恨の一撃！ </color>";
			return e_damage;
		}


		if (EnemyController.enemy_status ["at"] - PlayerContoroller.player_status ["df"] > 0) {
			e_damage = (int)(EnemyController.enemy_status ["at"] - PlayerContoroller.player_status ["df"] * Random.Range (0.6f, 1.5f)) + Random.Range (1, EnemyController.enemy_status ["ag"]);
		} else {
			e_damage = (int)(Random.Range(1, 3) * Random.Range (0.6f, 1.5f));
		}

		return e_damage;
	}

	public int compare_ag(){ // 素早さを比較して早い方を返す 1: player ,2: enemy
		return 	PlayerContoroller.player_status ["ag"] > EnemyController.enemy_status ["ag"] ? 1 : 2;
	}

	public void Player_die(){ // player死亡時に呼ばれる
		Debug.Log ("player is died");
	}

	public void Enemy_die(){ // enemy 死亡時に呼ばれる
		PlayerContoroller.player_status["exp"] += EnemyController.enemy_status["get_exp"];
		PlayerContoroller.player_status ["money"] += EnemyController.enemy_status ["get_money"];
		Check_lvup ();

	}

	public void Check_lvup(){ // レベルアップ処理
		foreach (int key in ExpController.exp_table.Keys) {
			if (PlayerContoroller.player_status ["lv"] == key) {
				if (PlayerContoroller.player_status ["exp"] >= ExpController.exp_table [key]) {
					PlayerContoroller.player_status ["lv"] = key + 1;
					StartCoroutine (Play_lvup ());
				}
			}
		}

	}
		

	public void Miss(){
		Debug.Log ("Miss");
	}

	IEnumerator Play_lvup(){
		sound_box.GetComponent<BattleSoundsController> ().LvUp ();
		yield return new WaitForSeconds (2.0f);
		for (int i = 0; i < StatusData.LvupPlayerStatus.GetLength(0); i++) {
			Debug.Log ("num " + StatusData.LvupPlayerStatus.GetLength(0));
			if (PlayerContoroller.player_status ["lv"] == StatusData.LvupPlayerStatus [i, 0]) {
				PlayerContoroller.player_status ["mhp"] += StatusData.LvupPlayerStatus [i, 1];
				PlayerContoroller.player_status ["mmp"] += StatusData.LvupPlayerStatus [i, 2];
				PlayerContoroller.player_status ["mat"] += StatusData.LvupPlayerStatus [i, 3];
				PlayerContoroller.player_status ["mdf"] += StatusData.LvupPlayerStatus [i, 4];
				PlayerContoroller.player_status ["mag"] += StatusData.LvupPlayerStatus [i, 5];
				log_obj.GetComponent<Text> ().text = string.Format ("{0}のレベルが<color=#fce700>{1}</color>にあがった！\n HP+{2} MP+{3}, ちから+{4} ぼうぎょ+{5} すばやさ+{6}", 
					PlayerContoroller.player_name, PlayerContoroller.player_status ["lv"], StatusData.LvupPlayerStatus [i, 1], StatusData.LvupPlayerStatus [i, 2], StatusData.LvupPlayerStatus [i, 3], StatusData.LvupPlayerStatus [i, 4], StatusData.LvupPlayerStatus [i, 5]);
			}
		}
	}

	private void BackField(){
		SceneManager.LoadScene ("Scene/" + SceneManager2d.current_scene);
	}
}
