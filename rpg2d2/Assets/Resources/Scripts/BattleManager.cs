using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {


	public Dictionary<string, int> player = new Dictionary<string, int> (PlayerContoroller.player_status);
	public Dictionary<string, int> enemy = new Dictionary<string, int> (EnemyController.enemy_status);
	public GameObject log_obj;
	public bool kaisin = false;
	public bool tukon = false;

	void Start(){
	}

	void Update(){
		
	}


	// たたかう が押された時に発火
	public void Fight(){
		int p_damage = P_damage ();
		int e_damage = E_damage ();
		Debug.Log ("p_damage " + p_damage);
		Debug.Log ("e_damage " + e_damage);

		int first = compare_ag ();
		if (first == 1) {

//			enemy["hp"] -= p_damage;
			if (enemy ["hp"] <= 0)
				Enemy_die ();
			
//			player ["hp"] -= e_damage;
			if (player ["hp"] <= 0)
				Player_die ();
			
			log_obj.GetComponent<Text> ().text = PlayerContoroller.player_name + "のこうげき\n" + EnemyController.monster_name + "に<color=#ff0000>" + p_damage + "</color>のダメージ\n" +
				EnemyController.monster_name + "のこうげき\n" + PlayerContoroller.player_name + "に<color=#ff0000>" + e_damage + "</color>のダメージ\n";
			
		} else {
//			player ["hp"] -= e_damage;
			if (player ["hp"] <= 0)
				Player_die ();
			
//			enemy["hp"] -= p_damage;

			if (enemy ["hp"] <= 0)
				Enemy_die ();

			log_obj.GetComponent<Text> ().text = EnemyController.monster_name + "のこうげき\n" + PlayerContoroller.player_name + "に<color=#ff0000>" + e_damage + "</color>のダメージ\n" +
				PlayerContoroller.player_name + "のこうげき\n" + EnemyController.monster_name + "に<color=#ff0000>" + p_damage + "</color>のダメージ\n";
		}
	}


	public int P_damage(){ // プレイヤーが与えるダメージ

		int p_damage = 0;

		// 会心の一撃
		if (Random.Range (1, 32) == 1) {
			p_damage = (int)(player ["at"] * Random.Range (0.6f, 1.5f)) + Random.Range (1, player ["ag"]);
			kaisin = true;
			return p_damage;
		}


		if (player ["at"] - enemy ["df"] > 0) {
			p_damage = (int)(player ["at"] - enemy ["df"] * Random.Range (0.6f, 1.5f)) + Random.Range (1, player ["ag"]);
		} else {
			p_damage = (int)(Random.Range(1, 3) * Random.Range (0.6f, 1.5f));
		}

		return p_damage;
			
	}

	public int E_damage(){
		int e_damage = 0;
		// 痛恨の一撃
		if (Random.Range (1, 32) == 1) {
			e_damage = (int)(enemy ["at"] * Random.Range (0.6f, 1.5f)) + Random.Range (1, enemy ["ag"]);
			tukon = true;
			return e_damage;
		}


		if (enemy ["at"] - player ["df"] > 0) {
			e_damage = (int)(enemy ["at"] - player ["df"] * Random.Range (0.6f, 1.5f)) + Random.Range (1, enemy ["ag"]);
		} else {
			e_damage = (int)(Random.Range(1, 3) * Random.Range (0.6f, 1.5f));
		}

		return e_damage;
	}

	public int compare_ag(){
		return 	player ["ag"] > enemy ["ag"] ? 1 : 2;
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
