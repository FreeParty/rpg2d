using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 静的変数 current_scene はフォールドでモンスターにエンカウントした時に更新されます(EncountController.cs)
 * 戦闘シーンに入った時にフィールドによってモンスターを切り替えるため、どのシーンでエンカウントしたかを保持します(EnemyController.cs)
 * 
 * isEncount はエンカウントしたかを判断する真偽値です
 */

public class SceneManager2d : MonoBehaviour {

	public static string current_scene = "main";
	public static bool isEncount;

	// Use this for initialization
	void Start () {
		current_scene = "main";
		isEncount = false;
	}

}
