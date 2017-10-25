using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Messeage : MonoBehaviour
{

    public string fileName = "test.txt";
    public bool encount = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show()
    {
        LogController.Callback callback = null;
        if (encount)
        {
            if (GetComponent<SymbolEncountContoller>() == null)
            {
                callback = GameObject.Find("Player").GetComponent<EncountController>().Encount;
            }
            else
            {
                callback = GetComponent<SymbolEncountContoller>().Encount;
            }
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