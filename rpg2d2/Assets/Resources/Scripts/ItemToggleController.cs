using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemToggleController : MonoBehaviour
{

    public int itemNo;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Select()
    {
        if (GetComponent<Toggle>().isOn)
        {
            GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            ItemList.Items item = ItemList.item_table[itemNo];
            Texture2D texture = Resources.Load(item.item_img) as Texture2D;
            GameObject.Find("ItemImage").GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            GameObject.Find("ItemImage").GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            GameObject.Find("Description").GetComponentInChildren<Text>().text = "【" + item.item_name + "】\n" + item.item_desc;

            switch (item.item_type)
            {
                case (int)ItemList.Eff.Hp_damage:
                case (int)ItemList.Eff.Df_up:
                case (int)ItemList.Eff.Ag_up:
                case (int)ItemList.Eff.At_up:
                    if (SceneManager.GetActiveScene().name != "battle")
                    {
                        GameObject.Find("Use").GetComponent<Button>().enabled = false;
                        GameObject.Find("Use").GetComponentInChildren<Text>().color = new Color(0.3f, 0.3f, 0.3f);
                    }
                    else
                    {
                        goto default;
                    }
                    break;
                case (int)ItemList.Eff.Key_item:
                    GameObject.Find("Use").GetComponent<Button>().enabled = false;
                    GameObject.Find("Remove").GetComponentInChildren<Text>().color = new Color(0.3f, 0.3f, 0.3f);
                    if (SceneManager.GetActiveScene().name != "battle")
                    {
                        GameObject.Find("Remove").GetComponent<Button>().enabled = false;
                        GameObject.Find("Remove").GetComponentInChildren<Text>().color = new Color(0.3f, 0.3f, 0.3f);
                    }
                    else
                    {
                        GameObject.Find("Back").GetComponent<Button>().enabled = false;
                        GameObject.Find("Back").GetComponentInChildren<Text>().color = new Color(0.3f, 0.3f, 0.3f);
                    }
                    break;
                default:
                    GameObject.Find("Use").GetComponent<Button>().enabled = true;
                    GameObject.Find("Use").GetComponentInChildren<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                    if (SceneManager.GetActiveScene().name != "battle")
                    {
                        GameObject.Find("Remove").GetComponent<Button>().enabled = true;
                        GameObject.Find("Remove").GetComponentInChildren<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                    }
                    else
                    {
                        GameObject.Find("Back").GetComponent<Button>().enabled = true;
                        GameObject.Find("Back").GetComponentInChildren<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                    }
                    break;
            }
        }
        else
        {
            GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0);
        }
    }
}