using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;

public class LogController : MonoBehaviour
{
    bool printed;
    int counter;
    string[] log;
    float timeElapsed;
    public static LogController logController;
    Text logBody;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (log != null)
        {
            timeElapsed += Time.deltaTime;
            if (!printed && timeElapsed > 0.1 && logBody.text.Length < log[counter].Length)
            {
                logBody.text = log[counter].Substring(0, logBody.text.Length + 1);
                if (counter == log.Length - 1 && logBody.text.Length == log[counter].Length)
                {
                    printed = true;
                }
                timeElapsed = 0;
            }
        }
    }

    void Next()
    {
        if (log != null)
        {
            if (printed)
            {
                gameObject.SetActive(false);
                log = null;
                if (callbackList.Count > 0)
                {
                    Callback function = callbackList[0];
                    callbackList.RemoveAt(0);
                    function();
                }
            }
            else if (GetComponentInChildren<Text>().text.Length == log[counter].Length)
            {
                counter++;
                GetComponentInChildren<Text>().text = "";
            }
            else
            {
                GetComponentInChildren<Text>().text = log[counter];
                if (counter == log.Length - 1) printed = true;
            }
        }
    }

    void init()
    {
        counter = 0;
        logBody = GameObject.Find("LogBody").GetComponent<Text>();
        logBody.text = log[counter].Substring(0, 1);
        printed = false;
    }

    public LogController printText(string[] str)
    {
        gameObject.SetActive(true);
        log = str;
        init();
        return this;
    }

    IEnumerator Load(string path)
    {
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

        log = messeage
            .Replace("#player_name#", PlayerContoroller.player_name)
            .Replace("\r\n", "\n")
            .Split(new string[] { "\n+_new_+\n" }, StringSplitOptions.RemoveEmptyEntries);
        init();
        yield return new WaitForSeconds(0f);
    }

    public LogController printTextByFileName(string fileName)
    {
        gameObject.SetActive(true);
        string path;
        #if UNITY_EDITOR
            path = Application.dataPath + "/StreamingAssets/Text/" + fileName;
        #elif UNITY_ANDROID
    	    path = "jar:file://" + Application.dataPath + "!/assets/Text/" + fileName;
        #elif UNITY_IPHONE
            path = path = Application.dataPath + "/Raw/Text/" + fileName;
        #else
            path = Application.dataPath + "/StreamingAssets/Text/" + fileName;
        #endif       
        StartCoroutine(Load(path));
        return this;
    }

    public delegate void Callback();

    public void cancel(Callback function)
    {
        callbackList = new List<Callback>();
        then(function);
    }

    public LogController then(Callback function)
    {
        if (function != null) callbackList.Add(function);
        return this;
    }

    private List<Callback> callbackList = new List<Callback>();
}