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
	public static string monster_name = "";
    public static int monster_num = -1;
    string prevSceneName;
    string[] encountStr = new string[] {"からまれた","殴り掛かられた","喧嘩を売られた","ガンを飛ばされた"};

    // Use this for initialization
    void Start() {
		prevSceneName =  GameObject.Find ("GameManager").GetComponent<GameManager> ().prevSceneName;
		string[,] monster_list = EnemiesData.GetMonsterList(prevSceneName);
        if (monster_num == -1)
        {
            monster_num = selectRandomMonster(monster_list);
        }
        else
        {
            for(int i = 0;i < monster_list.GetLength(0); i++)
            {
                if(int.Parse(monster_list[i,0]) == monster_num)
                {
                    monster_num = i;
                    break;
                }
            }
        }
		setEnemyStatus(monster_list, monster_num, prevSceneName);
        GameObject.Find("BattleField").transform.Find("LogModal").gameObject.GetComponent<LogController>().printText(new string[] { monster_name + "に" + encountStr[Random.Range(0, encountStr.Length - 1)] }).then(BattleManager.ToggleCommands); // 名前をlogにセット
    }

    /*
	 * Resources/enemys/シーン名 のディレクトリにモンスターの画像を格納してください
	 * また、シーンを複数作っていて、そのシーンによって出現するモンスターを切り替えたい場合、シーン名と上のシーン名を統一してください
	 * setEnemyStatus() でエンカウントしたシーンと、そのシーン名に同じResources/enemy/~ からモンスターの画像を引っ張ってきます
	*/

	public int selectRandomMonster(string[,] ml)
	{
        int mn = Random.Range(0, ml.GetLength(0));
        if (int.Parse(ml[mn, 0]) < 0)
        {
            return selectRandomMonster(ml);
        }
        else return mn;
	}

//[0]NO, [1] name, [2] HP, [3]MP, [4]attack, [5]guarg, [6]ag, [7]type, [8] drop_no, [9] get_exp, [10] get_money, 

	void setEnemyStatus(string [,] ml, int mn, string sceneName){
		monster_name = ml[mn, 1];
		Texture2D texture = Resources.Load("Sprites/enemies/"+ sceneName + "/" + ml[mn,0]) as Texture2D;
        GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
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
}
