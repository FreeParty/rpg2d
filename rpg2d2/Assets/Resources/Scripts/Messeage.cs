using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Messeage : MonoBehaviour
{

    public string fileName = "test.txt";
    public bool isFixed = true;
    Sprite[] sp;
    public string spriteName = "Sprites/npc/893";

    // Use this for initialization
    void Start()
    {
        sp = Resources.LoadAll<Sprite>(spriteName);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show()
    {
        LogController.Callback callback = null;
        if (GetComponent<SymbolEncountContoller>() != null)
        {
            callback = GetComponent<SymbolEncountContoller>().Encount;
        }
        if (!isFixed)
        {
            int dir = 0;
            Animator animator = GameObject.Find("Player").GetComponent<Animator>();
            int div = sp.Length / 4;
            if (animator.GetBool("walkingUnder"))
            {
                dir = div * 3;
            }
            else if (animator.GetBool("walkingLeft"))
            {
                dir = div * 2;
            }
            else if (animator.GetBool("walkingTop"))
            {
                dir = 0;
            }
            else
            {
                dir = div;
            }
            GetComponent<SpriteRenderer>().sprite = sp[dir];
        }
        LogController.logController.GetComponent<LogController>().printTextByFileName(fileName).then(callback);
    }

    /*
     public IEnumerator Show(){

		// TestData読み込み(ロード)
		yield return FileManager.ReadFileText(r => data_str = r, "/Text/" + fileName);

		LogController.Callback callback = null;
		if (encount)
		{
			callback = GameObject.Find("Player").GetComponent<EncountController>().Encount;
		}
		LogController.logController.GetComponent<LogController>().printText(data_str).then(callback);
	}
    */
}