using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBoxContoroller : MonoBehaviour {

	public int item_id = 1;
    public string spriteName = "Sprites/juelBox";
	Sprite[] sp;
	public bool isOpen = false;
	AudioSource audio;

    void Start(){
        gameObject.tag = "StrongBox";
		sp = Resources.LoadAll<Sprite>(spriteName);

        if (GameObject.Find("GameManager").GetComponent<GameManager>().strongBoxes != null && GameObject.Find("GameManager").GetComponent<GameManager>().strongBoxes.Contains(gameObject.name))
        {
            isOpen = true;
        }

        if (isOpen)
        {
            GetComponent<SpriteRenderer>().sprite = sp[1];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = sp[0];
        }
    }

    public void OpenBox(){
        if (!isOpen)
        {
            GetComponent<SpriteRenderer>().sprite = sp[1];
	    audio = GetComponent<AudioSource>();
	    audio.PlayOneShot(audio.clip);
            LogController.logController.printText(new string[]{ItemList.ItemName(item_id) + "を手に入れた！","やった！"});
	    PlayerContoroller.my_items.Add (item_id);
            isOpen = true;
        }
    }
}
