using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesData : MonoBehaviour {
	public static string[,] mainSceneMonsters = new string[,] {
		//[0]NO, [1] name, [2] HP, [3]MP, [4]attack, [5]guarg, [6]ag, [7]enemy_type, [8] drop_no, [9] get_exp, [10] get_money, [11] drop_probability_type
		{ "0", "スライム", 	"5", 	"0", 	"2", 		"2", 	"3", 	"0", 	"1", 		"1", 			"1",	"1"},
		{ "1", "もりのようせい", "12", "0", 	"8", 		"4", 	"1", 	"0", 	"1", 		"1", 			"5",	"2"},
		{ "2", "ありせんし", 		"7", "0", 	"6", 		"3", 	"4", 	"0", 	"2", 		"2", 			"2",	"3"},

	};
}
