using System;
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

    public IEnumerator Show()
    {
        string path = "";
        #if UNITY_EDITOR
            path = Application.dataPath + "/StreamingAssets/Text/" + fileName;
        #elif UNITY_ANDROID
    	    path = "jar:file://" + Application.dataPath + "!/assets/Text/" + fileName;
        #elif UNITY_IPHONE
            path = path = Application.dataPath + "/Raw/Text/" + fileName;
        #else
            path = Application.dataPath + "/StreamingAssets/Text/" + fileName;
        #endif

        string messeage = "";
        #if UNITY_EDITOR || UNITY_IPHONE
            StreamReader sr = new StreamReader(path, Encoding.GetEncoding("UTF-8"));
            messeage = sr.ReadToEnd();
            yield return new WaitForSeconds(0f);
        #elif UNITY_ANDROID
            WWW www = new WWW(path);
            yield return www;
            TextReader txtReader = new StringReader(www.text);
            messeage = txtReader.ReadToEnd();
        #endif

        string[] messeages = messeage
            .Replace("#player_name#", PlayerContoroller.player_name)
            .Replace("\r\n", "\n")
            .Split(new string[] { "\n+_new_+\n" }, StringSplitOptions.RemoveEmptyEntries);

        LogController.Callback callback = null;
        if (encount)
        {
            callback = GameObject.Find("Player").GetComponent<EncountController>().Encount;
        }
        LogController.logController.GetComponent<LogController>().printText(messeages).then(callback);
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