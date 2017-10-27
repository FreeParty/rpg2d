using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoroller : MonoBehaviour
{

    public static Dictionary<string, int> player_status = new Dictionary<string, int>() {
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
            if (touching.GetComponent<HealPointController>() != null)
            {
                touching.GetComponent<HealPointController>().Healing();
            }
            if (touching.GetComponent<Messeage>() != null)
            {
                touching.GetComponent<Messeage>().Show();
            }
            else if (touching.gameObject.GetComponent<SymbolEncountContoller>() != null)
            {
                touching.gameObject.GetComponent<SymbolEncountContoller>().Encount();
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