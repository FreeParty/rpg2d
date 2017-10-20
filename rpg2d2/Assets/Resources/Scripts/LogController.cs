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

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (!printed && timeElapsed > 0.1 && GetComponentInChildren<Text>().text.Length < log[counter].Length)
        {
            GetComponentInChildren<Text>().text = log[counter].Substring(0, GetComponentInChildren<Text>().text.Length + 1);
            if (counter == log.Length - 1 && GetComponentInChildren<Text>().text.Length == log[counter].Length) printed = true;
            timeElapsed = 0;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (printed)
            {
                gameObject.SetActive(false);
                if (callbacksList.Count > 0)
                {
                    Callbacks function = callbacksList[0];
                    callbacksList.Remove(callbacksList[0]);
                    function();
                }
                else if (callbackList.Count > 0)
                {
                    Callback function = callbackList[0];
                    callbackList.Remove(callbackList[0]);
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
        counter = 0;
        log = str;
        GetComponentInChildren<Text>().text = log[counter].Substring(0, 1);
        printed = false;
        return this;
    }

    public delegate void Callback();
    public delegate LogController Callbacks();

    public void cancel(Callback function)
    {
        callbackList = new List<Callback>();
        callbacksList = new List<Callbacks>();
        if (function != null) callbackList.Add(function);
    }

    public LogController then(Callback function)
    {
        callbackList.Add(function);
        return this;
    }
    public LogController then(Callbacks functions)
    {
        callbacksList.Add(functions);
        return this;
    }

    private Callback callback
    {
        get;
        set;
    }

    private List<Callback> callbackList = new List<Callback>();
    private List<Callbacks> callbacksList = new List<Callbacks>();

    private Callbacks callbacks
    {
        get;
        set;
    }
}