using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Messeage : MonoBehaviour
{

    public string fileName = "test.txt";
    private string path;
    private string[] messeage;
    public bool encount = false;
	public string[] data_str;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

	public IEnumerator Show(){

		// TestData読み込み(ロード)
		yield return FIleManager.ReadFileText(r => data_str = r, "/Text/test.txt");

		GameObject.Find("Window").transform.Find("LogWindow").gameObject.SetActive(true);

		LogController.Callback callback = null;
		if (encount)
		{
			callback = GameObject.Find("Player").GetComponent<EncountController>().Encount;
		}
		GameObject.Find("LogWindow").GetComponent<LogController>().printText(data_str).then(callback);


	}
}
