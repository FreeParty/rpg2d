﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
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

	public void Show(){
        string path = Application.dataPath + "/StreamingAssets/Text/" + fileName;
        StreamReader sr = new StreamReader(path, Encoding.GetEncoding("UTF-8"));
        string[] messeage = sr.ReadToEnd()
            .Replace("#player_name#", PlayerContoroller.player_name)
            .Replace("\r\n", "\n")
            .Split(new string[] { "\n+_new_+\n" }, StringSplitOptions.RemoveEmptyEntries);
        LogController.Callback callback = null;
		if (encount)
		{
			callback = GameObject.Find("Player").GetComponent<EncountController>().Encount;
		}
		LogController.logController.GetComponent<LogController>().printText(messeage).then(callback);
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
