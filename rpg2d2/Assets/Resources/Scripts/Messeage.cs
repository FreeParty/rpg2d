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
    // Use this for initialization
    void Start()
    {
        path = Application.dataPath + "/Resources/Text/" + fileName;
        StreamReader sr = new StreamReader(path, Encoding.GetEncoding("UTF-8"));
        messeage = sr.ReadToEnd()
            .Replace("#プレイヤー名#", PlayerContoroller.player_name)
            .Split(new string[] { "\n@改ページ@\n" }, StringSplitOptions.RemoveEmptyEntries);
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
            callback = GameObject.Find("Player").GetComponent<EncountController>().Encount;
        }
        LogController.logController.printText(messeage).then(callback);
    }
}
