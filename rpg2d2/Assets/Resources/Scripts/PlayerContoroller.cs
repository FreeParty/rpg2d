using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  PlayerContoroller : MonoBehaviour {

	public static Dictionary<string, int> player_status = new Dictionary<string, int> () {
		{"hp", 525},
		{"mhp", 25},
		{"mp", 0},
		{"mmp", 0},
		{"mat", 2},
		{"at", 2},
		{"mdf", 100},
		{"df", 100},
		{"mag", 4},
		{"ag", 3},
		{"lv", 1},
		{"exp", 0},
		{"money", 0}
	};

    public static List<int> my_items = new List<int>();
	public static string player_name = "sample";

    GameObject player_used;
    GameObject touching;

    void Awake(){
		DontDestroyOnLoad(this);
		player_used =  GameObject.Find ("Player_used");
        print (player_used != null) ;
		if (player_used) {
			player_used.GetComponent<Animator> ().enabled = true;
			Destroy (GameObject.Find ("Player"));
			player_used.name = "Player";
		}
    }

    public void CheckObject()
    {
        if (touching)
        {
            if (touching.GetComponent<OpenBoxContoroller>() != null)
            {
                touching.GetComponent<OpenBoxContoroller>().OpenBox();
            }
            if(touching.GetComponent<Messeage>() != null)
            {
                touching.GetComponent<Messeage>().Show();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        touching = coll.gameObject;
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        touching = null;
    }
}