using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBoxContoroller : MonoBehaviour {

	public string treasure_name = "item00";
	private Sprite[] sp;
    private bool isOpen;

    void Start(){
		sp = Resources.LoadAll<Sprite>("Sprites/juelBox");
        isOpen = false;
    }

	public void OpenBox(){
        if (!isOpen)
        {
            GetComponent<SpriteRenderer>().sprite = sp[1];
            GameObject.Find("Window").transform.Find("LogWindow").gameObject.SetActive(true);
            GameObject.Find("LogWindow").GetComponent<LogController>().printText(treasure_name + "を手に入れた！");
            isOpen = true;
        }
    }
}
