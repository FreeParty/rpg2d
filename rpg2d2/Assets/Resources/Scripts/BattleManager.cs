using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {

	public GameObject log_obj;
	public GameObject name_obj;
	public GameObject hp_obj;
	public GameObject mp_obj;
	public string kaisin = "";
	public string tukon = "";

	void Start(){
		name_obj.GetComponent<Text> ().text = PlayerContoroller.player_name;
		hp_obj.GetComponent<Text> ().text = PlayerContoroller.player_status["hp"].ToString ();
		mp_obj.GetComponent<Text> ().text = PlayerContoroller.player_status["mp"].ToString ();
	}


	// たたかう が押された時に発火
	public void Fight(){

		
		int p_damage = P_damage ();
		int e_damage = E_damage ();

		int first = compare_ag ();
		if (first == 1) { // player が先手

			EnemyController.enemy_status["hp"] -= p_damage;
			if (EnemyController.enemy_status ["hp"] <= 0)
				Enemy_die ();

			PlayerContoroller.player_status ["hp"] -= e_damage;
			if (PlayerContoroller.player_status ["hp"] <= 0)
				Player_die ();
			
			log_obj.GetComponent<Text> ().text = PlayerContoroller.player_name + "のこうげき\n" + kaisin + EnemyController.monster_name + "に<color=#ff0000>" + p_damage + "</color>のダメージ\n" +
				EnemyController.monster_name + "のこうげき\n" + tukon + PlayerContoroller.player_name + "に<color=#ff0000>" + e_damage + "</color>のダメージ\n";
			
		} else {
			PlayerContoroller.player_status ["hp"] -= e_damage;
			if (PlayerContoroller.player_status ["hp"] <= 0)
				Player_die ();


			EnemyController.enemy_status["hp"] -= p_damage;
			if (EnemyController.enemy_status ["hp"] <= 0)
				Enemy_die ();

			log_obj.GetComponent<Text> ().text = EnemyController.monster_name + "のこうげき\n" + tukon + PlayerContoroller.player_name + "に<color=#ff0000>" + e_damage + "</color>のダメージ\n" +
				PlayerContoroller.player_name + "のこうげき\n"+ kaisin + EnemyController.monster_name + "に<color=#ff0000>" + p_damage + "</color>のダメージ\n";
		}

		hp_obj.GetComponent<Text> ().text = PlayerContoroller.player_status ["hp"].ToString (); // HP 更新

		if (kaisin != "0" && tukon != "0") {
			kaisin = "";
			tukon = "";
		} else if (kaisin != "0") {
			kaisin = "";
		} else {
			tukon = "";
		}

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

	public int compare_ag(){
		return 	PlayerContoroller.player_status ["ag"] > EnemyController.enemy_status ["ag"] ? 1 : 2;
	}

	public void Player_die(){
		Debug.Log ("player is died");
	}

	public void Enemy_die(){
		Debug.Log ("Eenemy died");
	}

	public void Miss(){
		Debug.Log ("Miss");
	}
}
