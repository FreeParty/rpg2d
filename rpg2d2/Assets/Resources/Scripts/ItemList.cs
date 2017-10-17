﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour {

	public static List<Items> item_table = new List<Items>(){
		new Items (0, "なし", 0, (int)scene.Default, (int)Eff.Default, 0,""),
		new Items (1, "やくそう", 0, (int)scene.Battle, (int)Eff.Hp_heal, 30,"HPを30回復する"),
		new Items (2, "火炎瓶", 0, (int)scene.Battle, (int)Eff.Hp_damage, 30,"相手に30ダメージを与える"),
	};

	void Start(){
//		Debug.Log ("name" + item_table [2].item_name);
	}

	public enum Eff {
		Default,
		Hp_heal,
		Hp_damage,
		Mp_heal,
		Mp_damage,
		At_up,
		At_down,
		Df_up,
		Df_down,
		Ag_up,
		Ag_down
	}

	enum scene {
		Default,
		Battle,
		field,
	}


	public struct Items {
		public int item_id;
		public string item_name;
		public int item_cost;
		public int item_scene;
		public int item_type;
		public int item_effect;
        public string item_desc;

        public Items(int id, string name, int cost, int scene, int type, int effect,string desc)
        {
			item_id = id;
			item_name = name;
			item_cost = cost;
			item_scene = scene;
			item_type = type;
			item_effect = effect;
            item_desc = desc;
        }
	}
}
