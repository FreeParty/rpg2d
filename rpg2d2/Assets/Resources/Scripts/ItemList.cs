using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour {

	public static List<Items> item_table = new List<Items>(){
		//        1,id  2,Name 		3,cost 	4,Scene					5,type					6,effect	7,path				8,description
		new Items (0,	"なし",		0, 		(int)scene.Default, 	(int)Eff.Default, 		0,			"Images/test",		""),
		new Items (1,	"輸血液",	0, 		(int)scene.Battle, 		(int)Eff.Hp_heal, 		(int)(PlayerContoroller.player_status["hp"]*0.7),			"Images/item/ambubag",		"清潔なものではない"),
		new Items (2,	"でんだいどんぶり",	0, 		(int)scene.Battle, 		(int)Eff.Hp_heal, 		(int)(PlayerContoroller.player_status["hp"]*0.3),			"Images/item/gohan",		"白米に唐揚げをのせたもの\n使用によってHPの３割を回復する"),
		new Items (3,	"ドクターペッパー",		0, 		(int)scene.Battle, 		(int)Eff.Hp_heal, 	(int)(PlayerContoroller.player_status["hp"]*0.5),			"Images/item/drink",		"古い時代、世界線を行き来したという人物の飲み物\n使用によりHPを半分回復する\n"),
		new Items (4,	"ラーメン（武蔵屋）",		0, 		(int)scene.Battle, 		(int)Eff.Hp_heal, 	PlayerContoroller.player_status["hp"],			"Images/item/ramen",		"完全食\nHPを全回復する"),
		new Items (5,	"コーヒー",		0, 		(int)scene.Battle, 		(int)Eff.Mp_heal, 	(int)(PlayerContoroller.player_status["mp"]*0.3),			"Images/item/coffee",		"黒く濁ったそれは、MPを少量回復する"),
		new Items (6,	"トマトジュース",		0, 		(int)scene.Battle, 		(int)Eff.Mp_heal, 	(int)(PlayerContoroller.player_status["mp"]*0.5),			"Images/item/drink",		"赤く濁ったそれは、MPを中量回復する\nまるで血液のようだ"),
		new Items (7,	"ミスティックチェリー",		0, 		(int)scene.Battle, 		(int)Eff.Mp_heal, 	PlayerContoroller.player_status["mp"],			"Images/item/wine",		"遠い国の酒\nMPを全回復し、使用者に高揚感をもたらす"),
		new Items (8,	"石ころ",		0, 		(int)scene.Battle, 		(int)Eff.Hp_damage, 	3,			"Images/item/isi",		"ただの石ころ\n投げる以外に用途はない"),
		new Items (9,	"こぼれ落ちた単位",		0, 		(int)scene.Battle, 		(int)Eff.Hp_damage, 	1,			"Images/item/chabuutou",		"こぼれ落ちたものには誰も興味を示さない\nしかしある敵は過剰に反応するのだという"),
		new Items (10,	"彼の拳",		0, 		(int)scene.Battle, 		(int)Eff.Hp_damage, 	10,			"Images/item/genkotu",		"抵抗するのだ\n彼のように"),
		new Items (11,	"エルのクーポン",		0, 		(int)scene.Battle, 		(int)Eff.Hp_damage, 	7,			"Images/item/kippu",		"いまは亡き「エル」の遺物\nしかし彼らは今でも声高に主張しているのだという"),
		new Items (12,	"火炎瓶",		0, 		(int)scene.Battle, 		(int)Eff.Hp_damage, 	12,			"Images/item/kaen",		"使用により、敵に傷を負わせる"),
		new Items (13,	"手榴弾",		0, 		(int)scene.Battle, 		(int)Eff.Hp_damage, 	15,			"Images/item/m67hahensyuryuudan",		"使用により、敵に傷を負わせる\nこれはかつてこの地で起きた悲劇を象徴するものだ"),
		new Items (14,	"ロードローラー",		0, 		(int)scene.Battle, 		(int)Eff.Hp_damage, 	20,			"Images/item/auto",		"無駄無駄"),
		new Items (15,	"とん汁",		0, 		(int)scene.Battle, 		(int)Eff.At_up, 	(int)(PlayerContoroller.player_status["at"]*1.3),			"Images/item/ramen",		"力を一時的に少し上げる\nこいつはうまそうだ"),
		new Items (16,	"レッドブル",		0, 		(int)scene.Battle, 		(int)Eff.At_up, 	(int)(PlayerContoroller.player_status["at"]*1.5),			"Images/item/edrink",		"力を一時的に上げる\n力が、みなぎってきた"),
		new Items (17,	"あやしい白い薬",		0, 		(int)scene.Battle, 		(int)Eff.At_up, 	(int)(PlayerContoroller.player_status["at"]*2.0),			"Images/item/med_tablet",		"力を一時的に大きく上げる\n使うともう、後戻りできない"),
		new Items (18,	"防弾チョッキ",		0, 		(int)scene.Battle, 		(int)Eff.Df_up, 	(int)(PlayerContoroller.player_status["df"]*1.3),			"Images/item/boudanbest",		"守りを一時的に少し上げる\nこの街では、些か心もとない"),
		new Items (19,	"電磁気学基礎の単位",		0, 		(int)scene.Battle, 		(int)Eff.Df_up, 	(int)(PlayerContoroller.player_status["df"]*1.5),			"Images/item/datdocum",		"守りを一時的に上げる、希少なモノ\nこれを求める者は後を絶たない"),
		new Items (20,	"電磁気学応用の単位",		0, 		(int)scene.Battle, 		(int)Eff.Df_up, 	(int)(PlayerContoroller.player_status["df"]*2.0),			"Images/item/datdocum",		"守りを一時的に大きく上げる、希少なモノ\nしかし、基礎ほどの価値はないようだ"),
		new Items (21,	"クロックス",		0, 		(int)scene.Battle, 		(int)Eff.Ag_up, 	(int)(PlayerContoroller.player_status["ag"]*1.1),			"Images/item/kutu",		"素早さを一時的に少し上げる\n素早さとは、つまり敏捷であり攻?によって与えるダメージも少し大きくなる"),
		new Items (22,	"core i7",		0, 		(int)scene.Battle, 		(int)Eff.Ag_up, 	(int)(PlayerContoroller.player_status["ag"]*1.5),			"Images/item/icon414",		"素早さを一時的に上げる\n人業とは思えない高速かつ正確な動作を実現する。"),
		new Items (23,	"暗い木目の指輪",		0, 		(int)scene.Battle, 		(int)Eff.Ag_up, 	(int)(PlayerContoroller.player_status["ag"]*2.0),			"Images/item/ring",		"素早さを一時的に上げる\n古い時代、仮面巨人と呼ばれる者たちの指輪\nこの街にはいろいろな時代のものが流れ着く"),
		new Items (24,	"学生証",		0, 		(int)scene.Battle, 		(int)Eff.Key_item, 	1,			"Images/item/idcard",		"選ばれし者の証\nゲートを通ることができる\nゲートは多くの人にとって不要な物だったらしい。だが、果たして本当にそうなのだろうか"),
		new Items (25,	"6Fエレベーターの鍵",		0, 		(int)scene.Battle, 		(int)Eff.Key_item, 	1,			"Images/item/cardkey",		"1号館の1Fと6Fを繋ぐエレベーターを動かすことができる\nあの事件から、主要なエレベーターが封鎖されて久しい"),
		new Items (26,	"12Fエレベーターの鍵",		0, 		(int)scene.Battle, 		(int)Eff.Key_item, 	1,			"Images//cardkey",		"1号館の1Fと12Fを繋ぐエレベーターを動かすことができる\n少量の血痕が付着している"),
		new Items (27,	"通行許可証",		0, 		(int)scene.Battle, 		(int)Eff.Key_item, 	1,			"Images/item/dat_mail",		"駅東口の先へ進む事ができる\nかつて多くの路線により栄えていたこの駅だが、今では見る影もない"),
		new Items (28,	"GODの裁き",		0, 		(int)scene.Battle, 		(int)Eff.Hp_damage, 	999,			"Images/item/swd_tukuyomi",		"大学における偉大なる人物による裁き。\nその裁きは人の運命をも左右する。"),

	};

    public static string ItemName(int id)
    {
        string item_name = item_table.Find(x => x.item_id == id).item_name;
        return item_name;
    }

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
		Ag_down,
		Key_item,
	}

	enum scene {
		Default,
		Battle,
		Field,
	}


	public struct Items {
		public int item_id;
		public string item_name;
		public int item_cost;
		public int item_scene;
		public int item_type;
		public int item_effect;
        public string item_img;
        public string item_desc;

        public Items(int id, string name, int cost, int scene, int type, int effect,string img,string desc)
        {
			item_id = id;
			item_name = name;
			item_cost = cost;
			item_scene = scene;
			item_type = type;
			item_effect = effect;
            item_img = img;
            item_desc = desc;
        }
	}
}
