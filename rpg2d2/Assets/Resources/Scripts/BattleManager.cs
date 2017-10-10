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
				log_obj.GetComponent<Text>().text = PlayerContoroller.player_name + "のこうげき\n" + kaisin + EnemyController.monster_name + "に<color=#ff0000>" + p_damage + "</color>のダメージ\n" + 
					EnemyController.monster_name + "をたおした！\n" + "<color=#fce700>" + EnemyController.enemy_status["get_exp"] + "</color>の経験値と<color=#fce700>" + EnemyController.enemy_status["get_money"] + "</color>のゴールドを手に入れた！";
				Enemy_die ();
			} 

			PlayerContoroller.player_status ["hp"] -= e_damage;
			if (PlayerContoroller.player_status ["hp"] <= 0) { // player のhpが0になったとき
				Player_die ();
			} 

			if (PlayerContoroller.player_status ["hp"] >= 0 && EnemyController.enemy_status ["hp"] >= 0) {
				log_obj.GetComponent<Text> ().text = PlayerContoroller.player_name + "のこうげき\n" + kaisin + EnemyController.monster_name + "に<color=#ff0000>" + p_damage + "</color>のダメージ\n" +
					EnemyController.monster_name + "のこうげき\n" + tukon + PlayerContoroller.player_name + "に<color=#ff0000>" + e_damage + "</color>のダメージ\n";
			}
			
		} else {

			sound_box.GetComponent<BattleSoundsController> ().Attack ();
			PlayerContoroller.player_status ["hp"] -= e_damage;
			if (PlayerContoroller.player_status ["hp"] <= 0) {
				Player_die ();
			}


			EnemyController.enemy_status["hp"] -= p_damage;
			if (EnemyController.enemy_status ["hp"] <= 0) {
				log_obj.GetComponent<Text>().text = PlayerContoroller.player_name + "のこうげき\n" + kaisin + EnemyController.monster_name + "に<color=#ff0000>" + p_damage + "</color>のダメージ\n" + 
					EnemyController.monster_name + "をたおした！\n" + "<color=#fce700>" + EnemyController.enemy_status["get_exp"] + "</color>の経験値と<color=#fce700>" + EnemyController.enemy_status["get_money"] + "</color>のゴールドを手に入れた！";
				Enemy_die ();
			}

			if (PlayerContoroller.player_status ["hp"] >= 0 && EnemyController.enemy_status ["hp"] >= 0) {
				log_obj.GetComponent<Text> ().text = EnemyController.monster_name + "のこうげき\n" + tukon + PlayerContoroller.player_name + "に<color=#ff0000>" + e_damage + "</color>のダメージ\n" +
					PlayerContoroller.player_name + "のこうげき\n" + kaisin + EnemyController.monster_name + "に<color=#ff0000>" + p_damage + "</color>のダメージ\n";
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
					log_obj.GetComponent<Text>().text = PlayerContoroller.player_name + " にレベルが<color=#fce700>" + PlayerContoroller.player_status["lv"] + "</color>に上がった！\n";
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
	}

	private void BackField(){
		SceneManager.LoadScene ("Scene/" + SceneManager2d.current_scene);
	}
}
