using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoroller : MonoBehaviour
{

    public static Dictionary<string, int> player_status = new Dictionary<string, int>() {
        {"hp", 90},
        {"mhp", 20},
        {"mp", 0},
        {"mmp", 0},
        {"mat", 30},
        {"at", 30},
        {"mdf", 30},
        {"df", 30},
        {"mag", 30},
        {"ag", 30},
        {"lv", 20},
        {"exp", 0},
        {"money", 0}
    };

//    public static List<int> my_items = new List<int>();

	public static List<int> my_items = new List<int>{1,28};
    public static string player_name = "sample";
    GameObject touching;

    void Start()
    {
        if (GameObject.Find("Player") != gameObject)
        {
            GameObject.Find("Player").GetComponent<EncountController>().enabled = gameObject.GetComponent<EncountController>().enabled;
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
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
            else if (touching.GetComponent<HealPointController>() != null)
            {
                touching.GetComponent<HealPointController>().Healing();
            }
            else if (touching.GetComponent<Messeage>() != null)
            {
                touching.GetComponent<Messeage>().Show();
            }
            else if (touching.gameObject.GetComponent<SymbolEncountContoller>() != null)
            {
                touching.gameObject.GetComponent<SymbolEncountContoller>().Encount();
            }
            else if (touching.gameObject.GetComponent<HandItem>() != null)
            {
                touching.gameObject.GetComponent<HandItem>().Receive();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.GetComponent<OpenBoxContoroller>() != null ||
            coll.gameObject.GetComponent<Messeage>() != null ||
            coll.gameObject.GetComponent<SymbolEncountContoller>() != null ||
            coll.gameObject.GetComponent<HealPointController>())
        {
            touching = coll.gameObject;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        touching = null;
    }
}