using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesData : MonoBehaviour {
	public static string[,] mainSceneMonsters = new string[,] {
		//[0]NO, [1] img_path [2] name, [3] HP, [4]MP, [5]attack, [6]guarg, [7]ag, [8]type, [9] drop_no
		{ "0", "スライム", "5", "0", "4", "2", "3", "0", "0" },
		{ "1", "もりのようせい", "12", "0", "8", "4", "1", "0", "1"},
		{ "2", "ありせんし", "7", "0", "6", "3", "4", "0", "0"}

	};
}
