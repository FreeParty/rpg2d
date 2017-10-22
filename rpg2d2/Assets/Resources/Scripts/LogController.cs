using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogController : MonoBehaviour
{
    private bool printed;
    private int counter;
    private string[] log;
    private float timeElapsed;
    public static LogController logController;
    Text logBody;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (!printed && timeElapsed > 0.1 && logBody.text.Length < log[counter].Length)
        {
            logBody.text = log[counter].Substring(0, logBody.text.Length + 1);
            if (counter == log.Length - 1 && logBody.text.Length == log[counter].Length) printed = true;
            timeElapsed = 0;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (printed)
            {
                gameObject.SetActive(false);
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

    public LogController printText(string[] str)
    {
        gameObject.SetActive(true);
        counter = 0;
        log = str;
        logBody = GameObject.Find("LogBody").GetComponent<Text>();
        logBody.text = log[counter].Substring(0, 1);
        printed = false;
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
        if(function != null) callbackList.Add(function);
        return this;
    }

    private List<Callback> callbackList = new List<Callback>();
}